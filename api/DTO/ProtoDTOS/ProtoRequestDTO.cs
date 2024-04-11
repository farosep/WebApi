using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.ProtoDTOS
{
    public abstract class ProtoRequestDTO
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be longer than 5 characters")]
        [MaxLength(250, ErrorMessage = "Title cannot be over 250 characters")]

        public string Name { get; set; } = string.Empty;
    }
}