using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.CommentDTOs;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository : IProtoRepository<Comment, CreateCommentRequestDTO>
    {
    }
}