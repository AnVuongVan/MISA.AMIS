using System;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Lưu thông tin của người dùng
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class User: BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Tên đăng nhập
        /// </summary>
        [Required]
        [CheckDuplicate]
        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        /// [Required]
        [CheckDuplicate]
        [MinLength(6, "Mật khẩu phải tối thiểu 6 kí tự")]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        /// <summary>
        /// Chức vụ của người dùng
        /// </summary>
        public Guid PositionId { get; set; }      

        /// <summary>
        /// Quyền của người dùng
        /// </summary>
        public string RoleName { get; set; }
    }
}
