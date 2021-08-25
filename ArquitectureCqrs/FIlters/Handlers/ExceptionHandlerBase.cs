using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatronCqrs.FIlters.Handlers
{
    public class ExceptionHandlerBase
    {

        readonly Dictionary<int, string> RFC7231Types = new Dictionary<int, string>
        {
            {StatusCodes.Status404NotFound,
            "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4"},
            { StatusCodes.Status500InternalServerError,
            "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"}
        };

        public ValueTask SetResult(ExceptionContext context, int? status, string title
            , string detail = "")
        {
            var details = new ProblemDetails
            {
                Detail = detail,
                Status = status,
                Title = title,
                Type = RFC7231Types.ContainsKey(status.Value) ? RFC7231Types[status.Value] : string.Empty
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = status,
            };

            context.ExceptionHandled = true;
            return ValueTask.CompletedTask;

        }

    }
}
