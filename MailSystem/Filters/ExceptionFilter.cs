using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MailSystem.Core.Exceptions;
using MailSystem.Core.Resources;
using System.Data.Common;
using System.Net;

namespace MailSystem.API.Filters
{
    /// <summary>
    /// Class to handle all exception thrown
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly IResourceLocalizer _resource;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="resource">Object that contains all static messages</param>
        public ExceptionFilter(IResourceLocalizer resource)
        {
            _resource = resource;
        }

        /// <summary>
        /// Method to handle exception thrown
        /// </summary>
        /// <param name="context">Exception context</param>
        public void OnException(ExceptionContext context)
        {
            var status = HttpStatusCode.InternalServerError;
            var result = new { context.Exception.Message };

            if (context.Exception.GetType() == typeof(DbException))
            {
                status = HttpStatusCode.InternalServerError;
                result = new { context.Exception.Message };
            }

            if (context.Exception.GetType() == typeof(CustomException))
            {
                status = HttpStatusCode.BadRequest;
                result = new { context.Exception.Message };
            }

            var response = context.HttpContext.Response;

            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            context.Result = new JsonResult(result);
        }
    }
}
