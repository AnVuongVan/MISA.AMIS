using MISA.AMIS.Core.Enums;
using System.Collections.Generic;

namespace MISA.AMIS.Core.Entities
{
    public class Admin: BaseUser
    {
        public List<PermissionAction> PermissionActions {
            set
            {
                PermissionActions.Add(PermissionAction.CREATE);
                PermissionActions.Add(PermissionAction.UPDATE);
                PermissionActions.Add(PermissionAction.DELETE);
            }
            get
            {
                return PermissionActions;
            }
        }
    }
}
