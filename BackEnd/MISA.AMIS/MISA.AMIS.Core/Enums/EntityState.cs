using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Enums
{
    /// <summary>
    /// Xác định trạng thái của object
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public enum EntityState
    {
        /// <summary>
        /// Thêm mới
        /// </summary>
        Create = 1,

        /// <summary>
        /// Cập nhật
        /// </summary>
        Update = 2,

        /// <summary>
        /// Xóa
        /// </summary>
        Delete = 3
    }
}
