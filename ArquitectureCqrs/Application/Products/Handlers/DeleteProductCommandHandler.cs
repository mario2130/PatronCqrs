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
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {

        readonly IProductContext Context;
        public DeleteProductCommandHandler(IProductContext context) => Context = context;

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await Context.Remove(request.Id);
        }
    }
}
