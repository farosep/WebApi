
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
            var list = await SeleniumExtension.GetInfoFromCategory(MagnitMapExtension.MilkUrlId, MagnitMapExtension.MagnitDict);

            foreach (string s in list)
            {
                var ProductDto = await _repository.GetInfoFromTextAsync(
                    s,
                    ProductCategory.MilkAndEggs,
                    ProductCategory.SubCategoryMilk,
                    ProductBrands.MilkAndEggsBrends,
                    "магнит"
                    );

                await _repository.CreateAndUpdateAsync(ProductDto);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("scrap/MagnitBackery")]
        public async Task<IActionResult> ScrapMagnitBackery()
        {
            var list = await SeleniumExtension.GetInfoFromCategory(MagnitMapExtension.BreadUrlId, MagnitMapExtension.MagnitDict);

            foreach (string s in list)
            {
                var ProductDto = await _repository.GetInfoFromTextAsync(
                    s,
                    ProductCategory.Backery,
                    ProductCategory.SubCategoryBackery,
                    ProductBrands.BackeryBrands,
                    "Магнит"
                    );

                await _repository.CreateAndUpdateAsync(ProductDto);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // тут сейчас чушь магнитная
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
                var list = await SeleniumExtension.GetInfoFromCategory(UrlIds[i], MagnitMapExtension.MagnitDict);

                foreach (string s in list)
                {
                    var ProductDto = await _repository.GetInfoFromTextAsync(
                        s,
                        categories[i],
                        subcategories[i],
                        brands[i],
                        "магнит"
                        );

                    await _repository.CreateAndUpdateAsync(ProductDto);
                }

                await _context.SaveChangesAsync();
            }



            return Ok();
        }

        [HttpGet("scrap/LentaMilk")]
        public async Task<IActionResult> ScrapLentaMilk()
        {
            var list = await SeleniumExtension.GetInfoFromCategory(
                MagnitMapExtension.LentaDict["MilkCategoryId"],
                MagnitMapExtension.LentaDict);

            foreach (string s in list)
            {
                ProductDTO? productDTO = await _repository.GetInfoFromTextAsync(
                    s, ProductCategory.MilkAndEggs,
                    ProductCategory.SubCategoryMilk,
                    ProductBrands.MilkAndEggsBrends,
                    "лента"
                    );

                await _repository.CreateAndUpdateAsync(productDTO);
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