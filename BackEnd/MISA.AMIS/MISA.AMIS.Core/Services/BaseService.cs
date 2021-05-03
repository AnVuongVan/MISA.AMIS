using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using MISA.AMIS.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        IBaseRepository<T> _baseRepository;
        ServiceResult _serviceResult;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            this._baseRepository = baseRepository;
            this._serviceResult = new ServiceResult()
            {
                MISACode = MISACode.Success
            };
        }

        public virtual ServiceResult Add(T t)
        {
            t.EntityState = EntityState.Create;

            //Validate
            var isValidate = Validate(t);

            if (isValidate)
            {
                //Sinh ra giá trị cho trường khóa chính
                var keyProperty = t.GetType().GetProperty($"{typeof(T).Name}Id");
                keyProperty.SetValue(t, Guid.NewGuid());

                //Gán lại dữ liệu trả về
                _serviceResult.Data = _baseRepository.Add(t);
                _serviceResult.MISACode = MISACode.IsValid;
                _serviceResult.Body = t;
            }

            return _serviceResult;
        }

        public ServiceResult Delete(Guid id)
        {
            _serviceResult.Data = _baseRepository.Delete(id);
            return _serviceResult;
        }

        public IEnumerable<T> Get()
        {
            return _baseRepository.Get();
        }

        public T GetById(Guid id)
        {
            return _baseRepository.GetById(id);
        }

        public ServiceResult Update(T t)
        {
            t.EntityState = EntityState.Update;

            //Validate
            var isValidate = Validate(t);

            if (isValidate)
            {
                //Gán lại dữ liệu trả về
                _serviceResult.Data = _baseRepository.Update(t);
                _serviceResult.MISACode = MISACode.IsValid;
                _serviceResult.Body = t;
            }

            return _serviceResult;
        }

        /// <summary>
        /// Kiểm tra validate cho các trường
        /// </summary>
        /// <param name="t">object của T</param>
        /// <returns>true nếu dữ liệu hợp lệ, false nếu dữ liệu không hợp lệ</returns>
        private bool Validate(T t)
        {
            var isValid = true;
            var msgArrayError = new List<string>();

            //Đọc qua các property
            var properties = t.GetType().GetProperties();
            foreach (var property in properties)
            {
                //Lấy giá trị của từng trường
                var propertyValue = property.GetValue(t);
                var displayName = string.Empty;
                
                //Lấy ra tên hiển thị của những trường có attribute DisplayName
                var displayNameAttributes = property.GetCustomAttributes(typeof(DisplayName), true);
                if (displayNameAttributes.Length > 0)
                {
                    displayName = (displayNameAttributes[0] as DisplayName).Name;
                }

                //Kiểm tra xem các trường cần phải validate không
                //Kiểm tra bắt buộc nhập
                if (property.IsDefined(typeof(Required), false))
                {
                    if (string.IsNullOrEmpty(propertyValue.ToString()))
                    {
                        isValid = false;
                        msgArrayError.Add(string.Format(Properties.Resources.Msg_Required, displayName));
                        _serviceResult.MISACode = MISACode.NotValid;
                        _serviceResult.Message = Properties.Resources.Msg_IsNotValid;
                    }
                }

                //Kiểm tra trùng mã
                if (property.IsDefined(typeof(CheckDuplicate), false))
                {
                    //Kiểm tra trường dữ liệu có tồn tại không
                    var entityDuplicate = _baseRepository.GetEntityByProperty(t, property);

                    if (entityDuplicate != null)
                    {
                        isValid = false;
                        msgArrayError.Add(string.Format(Properties.Resources.Msg_Duplicate, displayName));
                        _serviceResult.MISACode = MISACode.NotValid;
                        _serviceResult.Message = Properties.Resources.Msg_IsNotValid;
                    }
                }

                //Kiểm tra độ dài dữ liệu
                if (property.IsDefined(typeof(MinLength), false))
                {
                    //Lấy độ dài dữ liệu từ entity
                    var attributeMinLength = property.GetCustomAttributes(typeof(MinLength), true)[0];
                    var length = (attributeMinLength as MinLength).Value;
                    var msg = (attributeMinLength as MinLength).ErrorMsg;

                    if (propertyValue.ToString().Trim().Length < length)
                    {
                        isValid = false;
                        msgArrayError.Add(msg ?? string.Format(Properties.Resources.Msg_Length, length));
                        _serviceResult.MISACode = MISACode.NotValid;
                        _serviceResult.Message = Properties.Resources.Msg_IsNotValid;
                    }
                }
            }

            _serviceResult.Data = msgArrayError;

            return isValid;
        }
    }
}
