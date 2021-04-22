using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

namespace MISA.AMIS.Controllers
{
    /// <summary>
    /// Api danh mục người dùng
    /// CreatedBy: VVAn(22/04/2021)
    /// </summary>
    public class UsersController : BaseEntityController<User>
    {
        IUserService _userService;

        public UsersController(IUserService userService): base(userService)
        {
            this._userService = userService;
        }
    }
}
