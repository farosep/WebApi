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
using api.Migrations;
using api.Models;
using api.Repository;
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

        [HttpGet("scrap/MagnitMilk")]
        public async Task<IActionResult> ScrapMagnitMilk()
        {
            var list = await SeleniumExtension.GetInfoFromCategory(MagnitMapExtension.MilkUrlId);

            foreach (string s in list)
            {
                (
                    var name,
                    var volume,
                    var percent,
                    var category,
                    var subCategory,
                    var brand,
                    var amount,
                    var weight,
                    var price
                ) = await _repository.GetInfoFromTextAsync(
                    s, ProductCategory.MilkAndEggs,
                    ProductCategory.SubCategoryMilk,
                    ProductBrands.MilkAndEggsBrends
                    );

                await _repository.CreateAndUpdateAsync(
                    name,
                    volume,
                    percent,
                    category,
                    subCategory,
                    brand,
                    amount,
                    weight,
                    price
                );
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("scrap/MagnitBackery")]
        public async Task<IActionResult> ScrapMagnitBackery()
        {
            var list = await SeleniumExtension.GetInfoFromCategory(MagnitMapExtension.BreadUrlId);

            foreach (string s in list)
            {
                (
                    var name,
                    var volume,
                    var percent,
                    var category,
                    var subCategory,
                    var brand,
                    var amount,
                    var weight,
                    var price
                ) = await _repository.GetInfoFromTextAsync(
                    s, ProductCategory.Backery,
                    ProductCategory.SubCategoryBackery,
                    ProductBrands.BackeryBrands
                    );

                await _repository.CreateAndUpdateAsync(
                    name,
                    volume,
                    percent,
                    category,
                    subCategory,
                    brand,
                    amount,
                    weight,
                    price
                );
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("scrap/MagnitAll")]
        public async Task<IActionResult> ScrapAllMagnit()
        {
            List<string> UrlIds = new List<string>
            {
                MagnitMapExtension.BreadUrlId,
                MagnitMapExtension.MilkUrlId
            };

            List<string> categories = new List<string>
            {
                ProductCategory.Backery,
                ProductCategory.MilkAndEggs,
            };

            List<List<string>> subcategories = new List<List<string>>
            {
                ProductCategory.SubCategoryBackery,
                ProductCategory.SubCategoryMilk
            };

            List<List<string>> brands = new List<List<string>>
            {
                ProductBrands.BackeryBrands,
                ProductBrands.MilkAndEggsBrends
            };

            for (int i = 0; i < categories.Count; i++)
            {
                var list = await SeleniumExtension.GetInfoFromCategory(UrlIds[i]);

                foreach (string s in list)
                {
                    (
                        var name,
                        var volume,
                        var percent,
                        var category,
                        var subCategory,
                        var brand,
                        var amount,
                        var weight,
                        var price
                    ) = await _repository.GetInfoFromTextAsync(
                        s, categories[i],
                        subcategories[i],
                        brands[i]
                        );

                    await _repository.CreateAndUpdateAsync(
                        name,
                        volume,
                        percent,
                        category,
                        subCategory,
                        brand,
                        amount,
                        weight,
                        price
                    );
                }

                await _context.SaveChangesAsync();
            }



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