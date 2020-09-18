using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Interfaces;
using WebApp3.Models;

namespace WebApp3.Repositories
{
    public class CommentRepository : IComment
    {
        private readonly ApplicationUserDbContext _applicationUserDbContext;

        public CommentRepository(ApplicationUserDbContext applicationUserDbContext)
        {
            _applicationUserDbContext = applicationUserDbContext;
        }
        public async Task<IEnumerable<Comment>> GetCommentByFanficIdAsync(int fanficId)
        {
            return await _applicationUserDbContext.Comments
                .Include(f => f.Fanfics)
                .Include(a => a.ApplicationUsers)
                .Where(f=>f.FanficId == fanficId).ToListAsync();
        }

        public IEnumerable<Comment> GetCommentByUserId(string userId)
        { 
            return _applicationUserDbContext.Comments
                .Include(a => a.ApplicationUsers)
                .Where(a=>a.ApplicationUserId == userId).ToList();
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            return await _applicationUserDbContext.Comments
                .Include(n => n.Fanfics)
                .FirstOrDefaultAsync(c => c.CommentId == commentId);
        }

        public void AddComment(Comment comment)
        {
            _applicationUserDbContext.Comments.Add(comment);
        }

        public void RemoveComment(Comment comment)
        {
            _applicationUserDbContext.Comments.Remove(comment);
        }
    }
}
