using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;
using api.Models;

namespace api.DTO.CommentDTOs
{
    public class CreateCommentRequestDTO : ProtoRequestDTO
    {
        public string Content { get; set; } = string.Empty;
    }
}