using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Interfaces;
using WebApp3.Models;

namespace WebApp3.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWork
    {
        private readonly ApplicationUserDbContext _applicationUserDbContext;
        private IFanfic _fanficRepository;
        private ITag _tagRepository;
        private ILike _likeRepository;
        private IComment _commentRepository;
        private IChapter _chapterRepository;
        private IFandom _fandomRepository;

        public UnitOfWorkRepository(ApplicationUserDbContext applicationUserDbContext)
        {
            _applicationUserDbContext = applicationUserDbContext;
        }
        public IFanfic Fanfic { get => _fanficRepository ??= new FanficRepository(_applicationUserDbContext);  }
        public ITag Tag { get => _tagRepository ??= new TagRepository(_applicationUserDbContext); }
        public ILike Like { get => _likeRepository ??= new LikeRepository(_applicationUserDbContext); }
        public IComment Comment { get => _commentRepository ??= new CommentRepository(_applicationUserDbContext); }
        public IChapter Chapter { get => _chapterRepository ??= new ChapterRepository(_applicationUserDbContext); }
        public IFandom Fandom { get => _fandomRepository ??= new FandomRepository(_applicationUserDbContext); }

        public async Task SaveAsync()
        {
           await _applicationUserDbContext.SaveChangesAsync();
        }
    }
}
