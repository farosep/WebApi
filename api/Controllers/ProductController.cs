using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using api.Data;
using api.DTO.ProductDTOs;
using api.Extensions;
using api.Extensions.Magnit;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(ApplicationDBContext context, IProductRepository repository, UserManager<AppUser> userManager) : ControllerBase
    {
        private readonly ApplicationDBContext _context = context;
        private readonly UserManager<AppUser> _userManager = userManager;
        private readonly IProductRepository _repository = repository;

        [HttpGet("scrapMagnitMilk")]
        public async Task<IActionResult> GetMilkInfo()
        {
            var list = await SeleniumExtension.GetInfoFromCategory(MagnitMapExtension.MilkUrl);

            var products = new List<Product>();
            foreach (string s in list)
            {

                int? weight = 0;
                int? volume = 0;
                int? amount = 0;
                string? category = "";
                float? percent = 0;

                var (price, pName) = s.GetPrice();
                // надо отрезать нужный текст и возвращать строку без части текста ( цена, объём и т.д)
                if (pName.IsLiquid())
                {
                    (volume, pName) = pName.GetLiquid();
                }
                else if (pName.IsSolid())
                {
                    (weight, pName) = pName.GetWeight();
                }
                else if (pName.IsAmount())
                {
                    (amount, pName) = pName.GetAmount();
                }
                if (pName.IsPercent())
                {
                    (percent, pName) = pName.GetPercent();
                }
                (category, pName) = pName.GetCategory();


                products.Add(new Product
                {
                    MagnitPrice = price,
                    Weight = weight,
                    Amount = amount,
                    Category = category,
                    Percent = percent,
                    Volume = volume,
                    Name = pName
                });
            }

            foreach (Product p in products)
            {
                await _repository.CreateAsync(p);

            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(QueryObject query)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var products = await _repository.GetAllAsync(appUser, query);
            var DTO = products.Select(p => p.ToProductDTO());

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var product = await _repository.GetByIdAsync(appUser, id);
            if (product == null) return NotFound();
            return Ok(product.ToProductDTO());
        }

        [HttpPut]
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
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var appUser = await _userManager.FindByNameAsync(User.GetUserName());
            if (await _repository.DeleteAsync(id, appUser) == null) return NotFound();

            return NoContent();
        }
    }
}