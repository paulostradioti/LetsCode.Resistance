using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LetsCode.Resistance.API
{
    public class CustomControllerBase : ControllerBase
    {
        [NonAction]
        public virtual InternalServerErrorObjectResult InternalServerError([ActionResultObjectValue] object error)
            => new InternalServerErrorObjectResult(error);
    }

    [DefaultStatusCode(DefaultStatusCode)]
    public class InternalServerErrorObjectResult : ObjectResult
    {
        private const int DefaultStatusCode = StatusCodes.Status500InternalServerError;

        public InternalServerErrorObjectResult(object value) : base(value)
        {
            StatusCode = DefaultStatusCode;
        }
    }
}