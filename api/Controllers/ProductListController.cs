using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.DTO.Stock;
using api.Mappers;
using api.DTO.ProductList;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers
{
    [Route("api/productList")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly IProductListRepository _plRepo;


        public ProductListController(ApplicationDBContext context, IProductListRepository plRepo)
        {
            _context = context;
            _plRepo = plRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductList()
        {
            var pl = await _plRepo.GetAllProductListsAsync();
            var plDTO = pl.Select(pl => pl.ToProductListDto());
            return Ok(pl);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductListById([FromRoute] int id)
        {
            var pl = await _context.ProductLists.FindAsync(id);

            if (pl == null)
            {
                return NotFound();
            }
            return Ok(pl);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductList([FromBody] CreateProductListRequestDTO PLDto)
        {
            var PLModel = PLDto.ToProductListFromCreateDTO();
            await _context.ProductLists.AddAsync(PLModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductListById), new { id = PLModel.Id }, PLModel.ToProductListDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateProductList([FromRoute] int id, [FromBody] UpdateProductListRequestDTO UpdateDto)
        {
            var model = await _context.ProductLists.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            if (UpdateDto.UserId != 0)
            {
                model.UserId = UpdateDto.UserId;
            }
            if (UpdateDto.Guid != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                model.Guid = UpdateDto.Guid;
            }

            await _context.SaveChangesAsync();

            return Ok(model.ToProductListDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var model = await _context.ProductLists.FirstOrDefaultAsync(x => x.Id == id);
            {
                if (model == null)
                {
                    return NotFound();
                }

                _context.ProductLists.Remove(model);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}