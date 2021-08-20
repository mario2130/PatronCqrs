using MediatR;
using PatronCqrs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatronCqrs.Application.Products.Queries
{
    public class GetProductByIdQuery: IRequest<Product>
    {
        public int Id;
    }
}
