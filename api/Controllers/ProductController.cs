using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductDTOs;
using api.DTO.StockDTOs;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(ApplicationDBContext context, IProductRepository repository) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;

        private readonly IProductRepository _repository = repository;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(QueryObject query)
        {
            var products = await _repository.GetAllAsync(query);
            var DTO = products.Select(p => p.ToProductDTO());

            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product.ToProductDTO());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ProductRequestDTO DTO)
        {
            var model = DTO.ToProductFromCreateDTO();
            await _repository.CreateAsync(model);
            return CreatedAtAction(nameof(GetById),
                                    new { id = model.Id },
                                    model.ToProductDTO());
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,
                                                    [FromBody] ProductRequestDTO DTO)
        {
            var model = await _repository.UpdateAsync(id, DTO);
            if (model == null) return NotFound();

            return Ok(model.ToProductDTO());
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (await _repository.DeleteAsync(id) == null) return NotFound();

            return NoContent();
        }
    }
}