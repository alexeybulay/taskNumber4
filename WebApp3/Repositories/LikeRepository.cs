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
    public class LikeRepository : ILike
    {
        private readonly ApplicationUserDbContext _applicationUserDbContext;

        public LikeRepository(ApplicationUserDbContext applicationUserDbContext)
        {
            _applicationUserDbContext = applicationUserDbContext;
        }


        public IEnumerable<Like> GetUserLikes(string userId)
        {
            return _applicationUserDbContext.Likes
                .Include(n => n.ApplicationUsers)
                .Where(n => n.ApplicationUserId == userId).ToList();
        }

        public IEnumerable<Like> GetFanficLikes(int fanficId)
        {
            return _applicationUserDbContext.Likes
                .Include(n => n.Fanfics)
                .Where(n => n.FanficId == fanficId).ToList();
        }

        public Like FindLike(int fanficId, string applicationUserId)
        {
            return _applicationUserDbContext.Likes
                .Include(n => n.Fanfics)
                .Include(a => a.ApplicationUsers)
                .FirstOrDefault(n => n.Fanfics.FanficId == fanficId && n.ApplicationUsers.Id == applicationUserId);
        }
        public void AddLike(Like like)
        {
            _applicationUserDbContext.Likes.Add(like);
        }

        public void RemoveLike(Like like)
        {
            _applicationUserDbContext.Likes.Remove(like);
        }
    }
}
