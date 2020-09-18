using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;

namespace WebApp3.Interfaces
{
    public interface IComment
    {
        Task<IEnumerable<Comment>> GetCommentByFanficIdAsync(int fanficId);
        IEnumerable<Comment> GetCommentByUserId(string userId);
        Task<Comment> GetCommentById(int commentId);
        void AddComment(Comment comment);
        void RemoveComment(Comment comment);
    }
}
