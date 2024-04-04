using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Data;

namespace api.Controllers
{
    [Route("api/productList")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private readonly ApplicationDBContext _context;


        public ProductListController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllProductList()
        {
            var pl = _context.ProductLists.ToList();

            return Ok(pl);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductListById([FromRoute] int id)
        {
            var pl = _context.ProductLists.Find(id);

            if (pl == null)
            {
                return NotFound();
            }
            return Ok(pl);
        }
    }
}