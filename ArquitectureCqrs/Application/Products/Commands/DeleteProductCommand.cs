using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatronCqrs.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id {  get; set; }
    }
}
