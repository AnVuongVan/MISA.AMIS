using MISA.AMIS.Core.Enums;
using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Quản lý thông tin chung của người dùng
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class BaseUser: BaseEntity
    {
        #region Property
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
        /// Chức vụ của người quản lý người dùng
        /// </summary>
        public Guid PositionParentId { get; set; }

        /// <summary>
        /// Công ty, tổ chức người dùng
        /// </summary>
        public Guid OfficeId { get; set; }

        /// <summary>
        /// Set hành vi mà người dùng được thao tác
        /// </summary>
        public List<PermissionAction> PermissionActions {
            set => PermissionActions.Add(PermissionAction.READ);
            get
            {
                return PermissionActions;
            }
        }
        #endregion
    }
}
