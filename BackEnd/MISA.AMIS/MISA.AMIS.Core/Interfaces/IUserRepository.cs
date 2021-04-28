using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Interfaces
{
    /// <summary>
    /// Interface người dùng
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public interface IUserRepository: IBaseRepository<User>
    {
        /// <summary>
        /// Đăng nhập cho người dùng
        /// </summary>
        /// <param name="userName">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>Thông tin của người dùng</returns>
        User Authenticate(string userName, string password);
    }
}
