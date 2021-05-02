using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Entities
{
    /// <summary>
    /// Map dữ liệu trả về cho client
    /// </summary>
    /// CreatedBy: VVAn(28/04/2021)
    public class TreeviewItem
    {
        /// <summary>
        /// Nội dung
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// Giá trị
        /// </summary>
        public Guid value { get; set; }

        /// <summary>
        /// Đóng, mở Treeview
        /// </summary>
        public bool collapsed { get; set; } = true;

        /// <summary>
        /// Treeview con
        /// </summary>
        public List<TreeviewItem> children { get; set; } = new List<TreeviewItem>();
    }
}
