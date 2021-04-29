using MISA.AMIS.Core.Entities;
using System;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces
{
    /// <summary>
    /// Interface service vị trí, chức vụ
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public interface IPositionService: IBaseService<Position>
    {
        /// <summary>
        /// Lấy ra cây chức vụ theo người dùng
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns>Cây phân cấp chức vụ</returns>
        Task<TreeviewItem> GetPositionById(Guid id);
    }
}
