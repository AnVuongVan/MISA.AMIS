using System;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lưu thông tin chức danh trong tổ chức
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class Position: BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid PositionId { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string PositionName { get; set; }
        #endregion
    }
}
