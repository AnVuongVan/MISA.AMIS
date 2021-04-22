using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

namespace MISA.AMIS.Infrastructure
{
    /// <summary>
    /// Lớp giao tiếp table office trong database
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class OfficeRepository: BaseRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(IConfiguration configuration): base(configuration)
        {

        }
    }
}
