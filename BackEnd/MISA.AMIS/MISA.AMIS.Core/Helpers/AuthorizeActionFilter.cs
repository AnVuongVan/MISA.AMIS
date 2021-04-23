using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MISA.AMIS.Core.Enums;

namespace MISA.AMIS.Core.Helpers
{
    public class AuthorizeActionFilter: IAuthorizationFilter
    {
        private readonly PermissionEntity _entity;
        private readonly PermissionAction _action;

        public AuthorizeActionFilter(PermissionEntity entity, PermissionAction action)
        {
            this._entity = entity;
            this._action = action;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //bool isAuthorized = MumboJumboFunction(context.HttpContext.User, _entity, _action);
            bool isAuthorized = false;

            if (!isAuthorized)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
