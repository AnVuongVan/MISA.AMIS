using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

namespace MISA.AMIS.Infrastructure
{
    /// <summary>
    /// Lớp giao tiếp table position trong database
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class PositionRepository: BaseRepository<Position>, IPositionRepository
    {
        public PositionRepository(IConfiguration configuration): base(configuration)
        {

        }
    }
}
