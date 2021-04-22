using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

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
    }
}
