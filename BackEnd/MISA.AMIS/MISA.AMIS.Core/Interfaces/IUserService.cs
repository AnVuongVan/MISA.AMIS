using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Interfaces
{
    /// <summary>
    /// Interface service người dùng
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public interface IUserService: IBaseService<User>
    {
        /// <summary>
        /// Đăng nhập cho người dùng
        /// </summary>
        /// <param name="userName">Tên đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns>Thông tin của người dùng</returns>
        public string Authenticate(string userName, string password);

        /// <summary>
        /// Lấy lên tất cả những người nằm dưới quyền của người dùng hiện tại
        /// </summary>       
        /// <param name="positionId">Khóa chính</param>
        /// <returns>Danh sách theo phân cấp cây</returns>
        IEnumerable<User> GetUsersByPositionId(Guid positionId);
    }
}
