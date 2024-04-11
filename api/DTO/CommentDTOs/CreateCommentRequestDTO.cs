using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;
using api.Models;

namespace api.DTO.CommentDTOs
{
    public class CreateCommentRequestDTO : ProtoRequestDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Content must be longer than 5 characters")]
        [MaxLength(250, ErrorMessage = "Content cannot be over 250 characters")]
        public string Content { get; set; } = string.Empty;
    }
}