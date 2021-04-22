using MISA.AMIS.Core.Enums;
using System;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Tạo chung các thuộc tính
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class BaseEntity
    {
        #region Property
        /// <summary>
        /// Ngày, giờ tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày, giờ chỉnh sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Trạng thái khi thêm, sửa, xóa
        /// </summary>
        public EntityState EntityState { get; set; }
        #endregion
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class Required: Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate: Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MinLength: Attribute
    {
        public int Value { get; set; }

        public string ErrorMsg { get; set; }

        public MinLength(int length, string errorMsg)
        {
            this.Value = length;
            this.ErrorMsg = errorMsg;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName: Attribute
    {
        public string Name { get; set; }

        public DisplayName(string name = null)
        {
            this.Name = name;
        }
    }
}
