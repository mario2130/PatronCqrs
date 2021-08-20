using MediatR;
using PatronCqrs.Application.Products.Commands;
using PatronCqrs.Context;
using PatronCqrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatronCqrs.Application.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        readonly IProductContext Context;
        public CreateProductCommandHandler(IProductContext context) => Context = context;

        public Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = command.Name,
                QuantityPerUnit = command.QuantityPerUnit,
                Description = command.Description,
                UnitPrice = command.UnitPrice,
                UnitsInStock = command.UnitsInStock,
                Discontinued = false
            };

           return Context.Add(product);
        }
    }
}
