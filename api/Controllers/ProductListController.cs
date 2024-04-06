using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.DTO.Stock;
using api.Mappers;
using api.DTO.ProductList;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult GetProductListById([FromRoute] int id)
        {
            var pl = _context.ProductLists.Find(id);

            if (pl == null)
            {
                return NotFound();
            }
            return Ok(pl);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateProductListRequestDTO PLDto)
        {
            var PLModel = PLDto.ToProductListFromCreateDTO();
            _context.ProductLists.Add(PLModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProductListById), new { id = PLModel.Id }, PLModel.ToProductListDto());
        }
    }
}