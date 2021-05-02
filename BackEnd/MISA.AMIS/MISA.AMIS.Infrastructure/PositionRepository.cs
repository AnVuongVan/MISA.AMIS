using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<TreeviewItem> GetPositionById(Guid id)
        {
            var treeviewItem = new TreeviewItem();
            var storeName = $"Proc_GetPositionById";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@PositionId", id);           
            dynamicParameters.Add("@PositionName", dbType: DbType.String, direction: ParameterDirection.Output);
            
            var positions = await _dbConnection.QueryAsync<TreeviewItem>(storeName, dynamicParameters, commandType: CommandType.StoredProcedure);
            treeviewItem.children = positions.ToList();
            treeviewItem.text = dynamicParameters.Get<string>("@PositionName");
            treeviewItem.value = id;

            return treeviewItem;
        }
    }
}
