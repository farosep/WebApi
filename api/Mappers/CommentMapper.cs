using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.CommentDTOs;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToCommentDto(this Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Name = commentModel.Name,
                Content = commentModel.Content,
                StockId = commentModel.StockId
            };
        }

        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDTO commentDTO, int stockId)
        {
            return new Comment
            {
                Name = commentDTO.Name,
                Content = commentDTO.Content,
                StockId = stockId
            };
        }
    }
}