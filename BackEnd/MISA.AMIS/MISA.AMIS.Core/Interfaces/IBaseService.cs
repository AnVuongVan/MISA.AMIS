using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Interfaces
{
    /// <summary>
    /// Base servie giao tiếp với controller
    /// </summary>
    /// <typeparam name="T">Đối tượng tương ứng entity</typeparam>
    /// CreatedBy: VVAn(22/04/2021)
    public interface IBaseService<T>
    {
        /// <summary>
        /// Lấy toàn bộ dữ liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Lấy dữ liệu theo khóa chính
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns>Bản ghi tương ứng</returns>
        T GetById(Guid id);

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="t">Dữ liệu cần thêm</param>
        /// <returns>Object trả về data, message, body và mã code</returns>
        ServiceResult Add(T t);

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="t">Dữ liệu muốn cập nhật</param>
        /// <returns>Object trả về data, message, body và mã code</returns>
        ServiceResult Update(T t);

        /// <summary>
        /// Xóa dữ liệu bản ghi
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns>Object trả về data, message, body và mã code</returns>
        ServiceResult Delete(Guid id);
    }
}
