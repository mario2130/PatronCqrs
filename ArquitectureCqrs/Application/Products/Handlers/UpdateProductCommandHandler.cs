using MediatR;
using PatronCqrs.Application.Products.Commands;
using PatronCqrs.Context;
using PatronCqrs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatronCqrs.Application.Products.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {

        readonly IProductContext Context;

        public UpdateProductCommandHandler(IProductContext productContext) => Context = productContext;

        public async Task<bool> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await Context.GetById(command.Id);
            var eval = false;

            if (product != null)
            {
                product.Name = command.Name;
                product.QuantityPerUnit = command.QuantityPerUnit;
                product.Description = command.Description;
                product.UnitPrice = command.UnitPrice;

                eval = await Context.Update(product);
            }
            else
            {
                throw new EntityNotFoundException("Product", command.Id);
            }
            return eval;
        }
    }
}
