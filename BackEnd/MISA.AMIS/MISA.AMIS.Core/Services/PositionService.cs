using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public Task<TreeviewItem> GetPositionById(Guid id)
        {           
            Task<TreeviewItem> treeviewItem = _positionRepository.GetPositionById(id);
            dynamic result = treeviewItem.Result;
            foreach (TreeviewItem item in result.children)
            {               
                item.children.AddRange(GetPositionById(item.value).Result.children);
            }           
            return treeviewItem;
        }

        public override ServiceResult Delete(Guid id)
        {            
            Task<TreeviewItem> treeviewItem = _positionRepository.GetPositionById(id);
            dynamic result = treeviewItem.Result;
            if (result.children.Count != 0)
            {
                foreach (TreeviewItem item in result.children)
                {
                    this.Delete(item.value);
                }
            }  
            return base.Delete(id);
        }
    }
}
