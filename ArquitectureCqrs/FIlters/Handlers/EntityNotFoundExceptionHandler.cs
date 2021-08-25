using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using PatronCqrs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatronCqrs.FIlters.Handlers
{
    public class EntityNotFoundExceptionHandler : ExceptionHandlerBase, IExceptionHandler
    {
        public Task Handle(ExceptionContext context)
        {
            EntityNotFoundException Exception = context.Exception as EntityNotFoundException;
            return SetResult(context, StatusCodes.Status404NotFound, "recurso no encontrado", $"recurso no encontrado {Exception.Entity} {Exception.Key}").AsTask();
        }
    }
}
