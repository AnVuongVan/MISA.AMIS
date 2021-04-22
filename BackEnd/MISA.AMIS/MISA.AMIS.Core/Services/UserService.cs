using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

namespace MISA.AMIS.Core.Services
{
    /// <summary>
    /// Service implement người sử dụng
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class UserService: BaseService<User>, IUserService
    {
        IUserRepository _userRepository;

        public UserService(IUserRepository userRepository): base(userRepository)
        {
            this._userRepository = userRepository;
        }
    }
}
