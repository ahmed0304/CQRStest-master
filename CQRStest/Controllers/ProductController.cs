using CQRStest.Commands;
using CQRStest.Handler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using CQRStest.Queries;
using CQRStest.Queries.Handler;
using Microsoft.AspNetCore.Authorization;
using MediatR;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRStest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        // GET: api/<ProductController>
        [HttpGet("getAll")]
        public async Task<ActionResult> GetAllProductInfo()
        {
            var result = await _mediator.Send(new AllProductDisplayQuery());

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // GET api/<ProductController>/5
        [HttpGet("getById/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _mediator.Send(new ProductDisplayQuery { Id = id });

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // POST api/<ProductController>
        [HttpPost("AddProduct")]
        public async Task<ActionResult> Post([FromBody] CreateProductCommand request)
        {
            if (request != null)
            {
                var result = await _mediator.Send(request);

                return CreatedAtAction(ControllerContext.ActionDescriptor.ActionName, result);
            }

            return BadRequest();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateProductCommand request)
        {
            if (id > 0 && request != null)
            {
                request.Id = id;

                var result = await _mediator.Send(request);
                if(result != null) return Ok(result);
            }

            return BadRequest("Does Not Exist");
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (id > 0)
            {
                bool result = await _mediator.Send(new DeleteProductCommand { Id = id });
                if (result)
                {
                    return Ok("Deleted Successfully");
                }
            }

            return NotFound();
        }


    }
}
