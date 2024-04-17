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
using api.Helpers;
using Microsoft.AspNetCore.Authorization;
using api.Extensions;
using Microsoft.AspNetCore.Identity;
using api.Models;
using api.Repository;

namespace api.Controllers
{
    [Route("api/productList")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        private readonly IProductListRepository _plRepo;

        private readonly IPLUserRepository _pLURepo;

        private readonly UserManager<AppUser> _userManager;


        public ProductListController(ApplicationDBContext context,
            IProductListRepository plRepo,
            UserManager<AppUser> userManager,
            IPLUserRepository pLUserRepo)
        {
            _context = context;
            _plRepo = plRepo;
            _userManager = userManager;
            _pLURepo = pLUserRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            var appUser = await _userManager.FindByNameAsync(User.GetUserName());
            var pl = await _plRepo.GetAllAsync(appUser, query);
            var answer = pl.Select(prlst => prlst.FromProductListToDTO(appUser));

            return Ok(answer);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetProductListById([FromRoute] int id)
        {
            var appUser = await _userManager.FindByNameAsync(User.GetUserName());
            var pl = await _plRepo.GetByIdAsync(appUser, id);
            if (pl == null) return NotFound();
            return Ok(pl);
        }



        // тут в теле можем получить айди листа но не обрабатываем его ибо зачем, бд сама выставит нужное значение
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductListDTO PLDto)
        {
            var appUser = await _userManager.FindByNameAsync(User.GetUserName());
            var PLModel = PLDto.ToProductListFromDTO(_context);
            await _plRepo.CreateAsync(PLModel);
            return CreatedAtAction(nameof(GetProductListById),
                                     new { id = PLModel.Id },
                                     PLModel.FromProductListToDTO(appUser));
        }


        // тут есть косяк что пут добавляет новые товары но не удаляет. => если есть привязка 1,2 а мы вводи 2,3 то всё упадёт 
        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ProductListDTO UpdateDto)
        {
            var appUser = await _userManager.FindByNameAsync(User.GetUserName());
            var model = await _plRepo.UpdateAsync(appUser, id, UpdateDto);
            if (model == null) return NotFound();

            await _context.SaveChangesAsync();

            return Ok(model.FromProductListToDTO(appUser));
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