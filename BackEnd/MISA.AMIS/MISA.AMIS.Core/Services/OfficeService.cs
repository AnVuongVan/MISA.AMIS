using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

namespace MISA.AMIS.Core.Services
{
    /// <summary>
    /// Service implement công ty, tổ chức
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class OfficeService: BaseService<Office>, IOfficeService
    { 
        IOfficeRepository _officeRepository;

        public OfficeService(IOfficeRepository officeRepository): base(officeRepository)
        {
            this._officeRepository = officeRepository;
        }
    }
}
