using Microsoft.AspNetCore.Mvc.Filters;
using PatronCqrs.FIlters.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatronCqrs.FIlters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {

        readonly IDictionary<Type, IExceptionHandler> ExceptionHandlers;

        public ApiExceptionFilterAttribute(Dictionary<Type, IExceptionHandler> exceptionHandlers)
            => ExceptionHandlers = exceptionHandlers;

        public override void OnException(ExceptionContext context)
        {
            //obtengo el tipo de exception viene, en este caso vendría
            //EntityNotFound k
            Type ExceptionType = context.Exception.GetType();

            // Buscar el manejador de la Excepcion
            if (ExceptionHandlers.ContainsKey(ExceptionType))
            {
                // el manejador de la excepcion
                ExceptionHandlers[ExceptionType].Handle(context);
            }
            else
            { 
                //excepcion sin manejadores
                //el estandar para el error de excepciones
                //1.- 
                //2.-
                //3.-
                //4.-
                //5.-



            }



            base.OnException(context);
        }

    }
}
