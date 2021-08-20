using MediatR;
using Microsoft.AspNetCore.Mvc;
using PatronCqrs.Application.Products.Commands;
using PatronCqrs.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatronCqrs.Controllers
{
    [Route("api/[Controller]")]
    public class ProductController : Controller
    {

        readonly IMediator Mediator;

        public ProductController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpPost("create-product")]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {

            int id = await Mediator.Send(command);
            if (id > 0)
                return Ok($"Producto creado con éxito {id} ");
            else
                return BadRequest("No se ha creado el producto");
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {

            if (await Mediator.Send(command))
                return Ok($"Producto actualizado con éxito");
            else
                return BadRequest("No se ha actualizado el producto");
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="command">commanda is Id product</param>
        /// <returns></returns>
        [HttpDelete("delete-product")]
        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }
    }
}
