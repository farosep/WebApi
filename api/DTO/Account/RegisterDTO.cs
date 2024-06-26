using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.ProtoDTOS;

namespace api.DTO.Account
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}