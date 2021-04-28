using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MISA.AMIS.Infrastructure
{
    /// <summary>
    /// Lớp giao tiếp table user trong database
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration): base(configuration)
        {

        }

        public User Authenticate(string userName, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserName", userName);
            parameters.Add("@Password", password);

            var user = _dbConnection.Query<User>($"Proc_Authenticate", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return user;
        }
    }
}
