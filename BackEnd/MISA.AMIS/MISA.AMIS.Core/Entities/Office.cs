using System;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lưu thông tin chi tiết về công ty, tổ chức 
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class Office: BaseEntity
    {
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid OfficeId { get; set; }

        /// <summary>
        /// Tên của công ty, tổ chức
        /// </summary>
        public string OfficeName { get; set; }
        #endregion
    }
}
