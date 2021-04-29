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

        public User Authenticate(string userName)
        {
            var user = _dbConnection.Query<User>($"Proc_Authenticate", new { UserName = userName }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return user;
        }

        public IEnumerable<string> GetPositionChildId(Guid positionId)
        {
            var childPotiontions = new List<string>();
            var listPositionsChild = _dbConnection.Query<Guid>($"Proc_GetPositionParentId", 
                new { PositionId = positionId }, commandType: CommandType.StoredProcedure);

            foreach(Guid item in listPositionsChild)
            {
                string positionItem = item.ToString();
                childPotiontions.Add(positionItem);
            }
            return childPotiontions;
        }

        public IEnumerable<User> GetUsersByPositionId(Guid positionId)
        {
            var users = _dbConnection.Query<User>($"Proc_GetUsersByPositionId", 
                new { PositionId = positionId }, commandType: CommandType.StoredProcedure);
            return users;
        }
    }
}
