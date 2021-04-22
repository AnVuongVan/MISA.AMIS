using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

namespace MISA.AMIS.Controllers
{
    /// <summary>
    /// Api danh mục công ty, tổ chức
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class OfficesController : BaseEntityController<Office>
    {
        IOfficeService _officeService;

        public OfficesController(IOfficeService officeService): base(officeService)
        {
            this._officeService = officeService;
        }
    }
}
