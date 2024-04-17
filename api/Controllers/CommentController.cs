using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.CommentDTOs;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockrepo, UserManager<AppUser> userManager)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockrepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(QueryObject query)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var comments = await _commentRepo.GetAllAsync(appUser, query);

            var commendDto = comments.Select(
                s => s.ToCommentDto());
            return Ok(commendDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var comment = await _commentRepo.GetByIdAsync(appUser, id);
            if (comment == null) return NotFound($"Comment with id:{id} does not exist");

            return Ok(comment.ToCommentDto());
        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId,
            CreateCommentRequestDTO commentDTO)
        {
            if (!await _stockRepo.IsExist(stockId)) return BadRequest("Stock does not exists");

            var commentModel = commentDTO.ToCommentFromCreateDTO(stockId);
            await _commentRepo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById),
                new { id = commentModel },
                commentModel.ToCommentDto());

        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _commentRepo.DeleteAsync(id);
            if (commentModel == null) return NotFound($"Comment with id:{id} does not exist");

            return Ok();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id,
            [FromBody] CreateCommentRequestDTO requesstDTO)
        {
            var username = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(username);
            var comment = await _commentRepo.UpdateAsync(appUser, id, requesstDTO);
            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDto());
        }
    }
}