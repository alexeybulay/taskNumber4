using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;

namespace WebApp3.Interfaces
{
    public interface ILike
    {
        IEnumerable<Like> GetUserLikes (string userId);
        IEnumerable<Like> GetFanficLikes (int fanficId);
        Like FindLike(int fanficId, string applicationUserId); 
        void AddLike(Like like);
        void RemoveLike(Like like);
    }
}
