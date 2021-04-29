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
        /// <returns>Thông tin của người dùng</returns>
        User Authenticate(string userName);

        /// <summary>
        /// Lấy ra vị trí cấp dưới của cây phân cấp
        /// </summary>
        /// <param name="positionId">Vị trí người dùng hiện tại</param>
        /// <returns>Danh sách vị trí cấp dưới</returns>
        IEnumerable<string> GetPositionChildId(Guid positionId);

        /// <summary>
        /// Lấy danh sách người dùng có cùng vị trí
        /// </summary>
        /// <param name="positionId">Vị trí người dùng</param>
        /// <returns>Danh sách người dùng</returns>
        IEnumerable<User> GetUsersByPositionId(Guid positionId);
    }
}
