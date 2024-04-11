using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.DTO.StockDTOs;
using api.Mappers;
using api.DTO.ProductListDTOs;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using api.DTO.ProductListDtos;

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
        public async Task<IActionResult> GetAll()
        {
            var pl = await _plRepo.GetAllAsync();
            var plDTO = pl.Select(pl => pl.ToProductListDto());
            return Ok(pl);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductListById([FromRoute] int id)
        {
            var pl = await _plRepo.GetByIdAsync(id);

            if (pl == null) return NotFound();
            return Ok(pl);
        }

        // тут надо убрать заполнение продуктами, вместо него вставить список интов нужных продуктов
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductListRequestDTO PLDto)
        {
            var PLModel = PLDto.ToProductListFromCreateDTO(_context);
            await _context.ProductLists.AddAsync(PLModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductListById),
                                     new { id = PLModel.Id },
                                     PLModel.ToProductListDto());
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductListDTO UpdateDto)
        {
            var model = await _plRepo.UpdateAsync(id, UpdateDto);
            if (model == null) return NotFound();

            await _context.SaveChangesAsync();

            return Ok(model.ToProductListDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var model = await _plRepo.DeleteAsync(id);
            {
                if (model == null) return NotFound();

                return NoContent();
            }
        }
    }
}