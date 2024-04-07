using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.ProtoDTOS
{
    public abstract class ProtoRequestDTO
    {
        public string Name { get; set; } = string.Empty;
    }
}