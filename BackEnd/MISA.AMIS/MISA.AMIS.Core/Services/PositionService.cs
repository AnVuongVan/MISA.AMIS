using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;

namespace MISA.AMIS.Core.Services
{
    /// <summary>
    /// Service implement vị trí, chức vụ
    /// </summary>
    /// CreatedBy: VVAn(22/04/2021)
    public class PositionService: BaseService<Position>, IPositionService
    {
        IPositionRepository _positionRepository;
        public PositionService(IPositionRepository positionRepository): base(positionRepository)
        {
            this._positionRepository = positionRepository;
        }
    }
}
