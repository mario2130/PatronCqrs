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
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {

        readonly IProductContext Context;
        public GetAllProductsQueryHandler(IProductContext context) => Context = context;

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await Context.GetAll();
        }
    }
}
