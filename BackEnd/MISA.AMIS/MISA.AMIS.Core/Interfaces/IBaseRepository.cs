using System;
using System.Collections.Generic;
using System.Reflection;

namespace MISA.AMIS.Core.Interfaces
{
    /// <summary>
    /// Base để giao tiếp với database
    /// </summary>
    /// <typeparam name="T">Đối tượng bảng tương ứng trong database</typeparam>
    /// CreatedBy: VVAn(22/04/2021)
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Lấy dữ liệu theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính của bảng</param>
        /// <returns>Bản ghi tương ứng</returns>
        T GetById(Guid id);

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="t">Dữ liệu cần thêm</param>
        /// <returns>0 nếu thêm thất bại, ngược lại thêm thành công</returns>
        int Add(T t);

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="t">Dữ liệu muốn cập nhật</param>
        /// <returns>0 nếu cập nhật thất bại, ngược lại cập nhật thành công</returns>
        int Update(T t);

        /// <summary>
        /// Xóa dữ liệu bản ghi
        /// </summary>
        /// <param name="id">Khóa chính của bảng</param>
        /// <returns>0 nếu xóa thất bại, ngược lại nếu xóa thành công</returns>
        int Delete(Guid id);

        /// <summary>
        /// Lấy dữ liệu theo một thuộc tính nào đó
        /// </summary>
        /// <param name="t">object đối tượng tham chiếu</param>
        /// <param name="property">Thuộc tính của đối tượng</param>
        /// <returns>Bản ghi tìm được theo thuộc tính đó</returns>
        T GetEntityByProperty(T t, PropertyInfo property);
    }
}
