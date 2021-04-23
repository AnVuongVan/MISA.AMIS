using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.Core.Helpers
{
    public class AuthorizeAttribute: TypeFilterAttribute
    {
        public AuthorizeAttribute(PermissionEntity permisstionEntity, PermissionAction permissionAction): base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { permisstionEntity, permissionAction };
        }
    }
}
