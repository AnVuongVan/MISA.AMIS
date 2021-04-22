using Microsoft.AspNetCore.Http;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enums;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MISA.AMIS.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(
                new ServiceResult
                {
                    Data = new
                    {
                        devMsg = ex.Message,
                        cusMsg = "Có lỗi xảy ra vui lòng liên hệ với MISA"
                    },
                    Message = "Có lỗi xảy ra vui lòng liên hệ với MISA",
                    MISACode = MISACode.Exception,
                }
            );
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
