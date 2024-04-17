using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductDTOs;
using api.DTO.StockDTOs;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(ApplicationDBContext context, IProductRepository repository, UserManager<AppUser> userManager) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;

        private readonly IProductRepository _repository = repository;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(QueryObject query)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var products = await _repository.GetAllAsync(appUser, query);
            var DTO = products.Select(p => p.ToProductDTO());

            return Ok(products);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var product = await _repository.GetByIdAsync(appUser, id);
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
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var model = await _repository.UpdateAsync(appUser, id, DTO);
            if (model == null) return NotFound();

            return Ok(model.ToProductDTO());
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var appUser = await _userManager.FindByNameAsync(User.GetUserName());
            if (await _repository.DeleteAsync(id, appUser) == null) return NotFound();

            return NoContent();
        }
    }
}