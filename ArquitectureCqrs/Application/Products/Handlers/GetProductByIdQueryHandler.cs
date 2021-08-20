using MediatR;
using PatronCqrs.Application.Products.Queries;
using PatronCqrs.Context;
using PatronCqrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatronCqrs.Application.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {

        readonly IProductContext Context;
        public GetProductByIdQueryHandler(IProductContext context) => Context = context;

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await Context.GetById(request.Id);
        }
    }
}
