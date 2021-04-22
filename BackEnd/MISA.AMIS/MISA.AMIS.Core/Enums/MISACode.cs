using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Enums
{
    /// <summary>
    /// MISACode để xác định trạng thái khi validate
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public enum MISACode
    {
        /// <summary>
        /// Dữ liệu hợp lệ
        /// </summary>
        IsValid = 100,

        /// <summary>
        /// Dữ liệu không hợp lệ
        /// </summary>
        NotValid = 900,

        /// <summary>
        /// Thành công
        /// </summary>
        Success = 200,

        /// <summary>
        /// Lỗi server
        /// </summary>
        Exception = 500,
    }
}
