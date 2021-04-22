using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MISA.AMIS.Infrastructure
{
    /// <summary>
    /// Base giao tiếp với database
    /// </summary>
    /// <typeparam name="T">Đối tượng tương ứng trong database</typeparam>
    /// CreatedBy: VVAn(22/04/2021)
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T:BaseEntity
    {
        #region Declare
        IConfiguration _configuration;
        string _connectionString = string.Empty;
        protected IDbConnection _dbConnection = null;
        string _tableName;
        #endregion

        #region Constructor
        public BaseRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = _configuration.GetConnectionString("MISAAMISConnectionString");
            this._dbConnection = new MySqlConnection(_connectionString);
            this._tableName = typeof(T).Name;
        }
        #endregion

        #region Method
        public int Add(T t)
        {
            var rowEffect = 0;
            //Khởi tạo kết nối database
            _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    //Map dữ liệu để gửi xuống database
                    var parameters = MapperToDatabase(t);

                    //Thực thi command
                    rowEffect = _dbConnection.Execute($"Proc_Insert{_tableName}", parameters, commandType: CommandType.StoredProcedure);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }
            }

            //Trả về số bản ghi được thêm mới
            return rowEffect;
        }

        public int Delete(Guid id)
        {
            var rowEffect = 0;

            //Set param gửi xuống database
            string _idName = _tableName + "Id";
            var parameter = new DynamicParameters();
            parameter.Add(_idName, id);

            //Khởi tạo kết nối database
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    //Thực thi command
                    rowEffect = _dbConnection.Execute($"Proc_Delete{_tableName}", parameter, commandType: CommandType.StoredProcedure);
                    
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }              
            }

            //Trả về số bản ghi bị xóa
            return rowEffect;
        }

        public IEnumerable<T> Get()
        {
            //Lấy dữ liệu 
            var entities = _dbConnection.Query<T>($"Proc_Get{_tableName}", commandType: CommandType.StoredProcedure);
            return entities;
        }

        public T GetById(Guid id)
        {
            //Set param gửi xuống database
            string _idName = _tableName + "Id";
            var parameter = new DynamicParameters();
            parameter.Add(_idName, id);

            //Lấy dữ liệu
            var entity = _dbConnection.Query<T>($"Proc_Get{_tableName}ById", parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return entity;
        }

        public T GetEntityByProperty(T t, PropertyInfo property)
        {
            //Lấy tên thuộc tính 
            var propertyName = property.Name;
            //Lấy giá trị thuộc tính
            var propertyValue = property.GetValue(t);
            //Lấy giá trị khóa chính
            var keyValue = t.GetType().GetProperty($"{_tableName}Id").GetValue(t);

            //Câu lệnh query sql
            string query;

            if (t.EntityState == EntityState.Create)
            {
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}'";
            }
            else if (t.EntityState == EntityState.Update)
            {
                query = $"SELECT * FROM {_tableName} WHERE {propertyName} = '{propertyValue}' AND {_tableName}Id <> '{keyValue}'";
            }
            else
                return null;

            var entity = _dbConnection.Query<T>(query, commandType: CommandType.Text).FirstOrDefault();
            return entity;
        }

        public int Update(T t)
        {
            var rowEffect = 0;
            //Khởi tạo kết nối database
            _dbConnection.Open();

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    //Map dữ liệu để gửi xuống database
                    var parameters = MapperToDatabase(t);

                    //Thực thi command
                    rowEffect = _dbConnection.Execute($"Proc_Update{_tableName}", parameters, commandType: CommandType.StoredProcedure);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                }
            }

            //Trả về số bản ghi được cập nhật
            return rowEffect;
        }

        /// <summary>
        /// Map dữ liệu gửi từ client, đóng gói gửi xuống database
        /// </summary>
        /// <param name="t">Dữ liệu gửi từ client</param>
        /// <returns>Dữ liệu được đóng gói</returns>
        private DynamicParameters MapperToDatabase(T t)
        {
            var parameters = new DynamicParameters();
            var properties = t.GetType().GetProperties();

            //Xử lý các kiểu dữ liệu
            foreach (var property in properties)
            {
                //Lấy tên thuộc tính
                var propertyName = property.Name;
                //Lấy giá trị của thuộc tính
                var propertyValue = property.GetValue(t);
                //Lấy kiểu dữ liệu của thuộc tính
                var propertyType = property.PropertyType;

                if (propertyType == typeof(Guid) || propertyType == typeof(Guid?))
                {
                    parameters.Add($"@{propertyName}", propertyValue, DbType.String);
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    var dbValue = (bool) propertyValue == true ? 1 : 0;
                    parameters.Add($"@{propertyName}", dbValue, DbType.Int32);
                }
                else
                {
                    parameters.Add($"@{propertyName}", propertyValue);
                }
            }

            return parameters;
        }

        public void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
        #endregion
    }
}
