using MediatR;
using PatronCqrs.Application.Products.Commands;
using PatronCqrs.Context;
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

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await Context.GetById(request.Id);
            var eval = false;

            if (product != null)
            {
                product.Name = request.Name;
                product.QuantityPerUnit = request.QuantityPerUnit;
                product.Description = request.Description;
                product.UnitPrice = request.UnitPrice;

                eval = await Context.Update(product);
            }
            return eval;
        }
    }
}
