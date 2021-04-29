using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;
using System;
using System.Security.Claims;

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

        public override IActionResult Get()
        {
            string positionId = base.GetPositionId();
            return Ok(_userService.GetUsersByPositionId(Guid.Parse(positionId)));
        }

        [HttpPost("{login}")]
        public IActionResult Authenticate(User user)
        {
            var token = _userService.Authenticate(user.UserName, user.Password);

            if (token == string.Empty)
                return BadRequest(new { message = "Tên đăng nhập hoặc mật khẩu không đúng." });
            return Ok(new { token });
        }
    }
}
