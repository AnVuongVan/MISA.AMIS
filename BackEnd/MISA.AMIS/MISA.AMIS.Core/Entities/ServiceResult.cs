using MISA.AMIS.Core.Enums;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Tạo dữ liệu trả về
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class ServiceResult
    {
        /// <summary>
        /// Lỗi trả về
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Body { get; set; }

        /// <summary>
        /// Thông điệp trả về
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Mã code trả về
        /// </summary>
        public MISACode MISACode { get; set; }
    }
}
