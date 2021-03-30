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
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CQRStest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductsCommandHandler _commandHandler;
        private ProductQueryHandler _queryHandler;
        public ProductController(ProductsCommandHandler commandHandler, ProductQueryHandler queryHandler)
        {
            this._commandHandler = commandHandler;
            this._queryHandler = queryHandler;
        }


        // GET: api/<ProductController>
        [HttpGet("getAll")]
        public ActionResult GetAllProductInfo()
        {
            var result = _queryHandler.Handler();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // GET api/<ProductController>/5
        [HttpGet("getById/{id}")]
        public ActionResult Get(int id)
        {
            ProductDisplayQuery lv_productDisplay = new ProductDisplayQuery { Id = id };
            var result = _queryHandler.Handler(lv_productDisplay);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound();
        }

        // POST api/<ProductController>
        [HttpPost("AddProduct")]
        public ActionResult Post([FromBody] CreateProductCommand request)
        {
            if (request != null)
            {
               int product_id = _commandHandler.Handler(request);
               return CreatedAtAction(ControllerContext.ActionDescriptor.ActionName, product_id);
            }

            return BadRequest();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] UpdateProductCommand request)
        {
            if (id > 0 && request !=null)
            {
                bool result  = _commandHandler.Handler(id,request);
                if (result)
                {
                    return CreatedAtAction(ControllerContext.ActionDescriptor.ActionName, id);
                }
            }

            return BadRequest();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                bool result = _commandHandler.Handler(new DeleteProductCommand { Id=id });
                if (result)
                {
                    return Ok(id);
                }
            }

            return NotFound();
        }


    }
}
