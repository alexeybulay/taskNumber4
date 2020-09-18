using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Interfaces;
using WebApp3.Models;

namespace WebApp3.Repositories
{
    public class ChapterRepository : IChapter
    {
        private readonly ApplicationUserDbContext _applicationUserDbContext;

        public ChapterRepository(ApplicationUserDbContext applicationUserDbContext)
        {
            _applicationUserDbContext = applicationUserDbContext;
        }
        public IEnumerable<Chapter> GetChapters(Fanfic fanfic)
        {
            return _applicationUserDbContext.Chapters.ToList()
                .Where(n => n.FanficId == fanfic.FanficId);
        }

        public Chapter GetChapterById(int id)
        {
            return _applicationUserDbContext.Chapters.FirstOrDefault(n => n.ChapterId == id);
        }

        public void AddChapter(Chapter chapter)
        {
            _applicationUserDbContext.Chapters.Add(chapter);
        }

        public void Update(Chapter chapter)
        {
            _applicationUserDbContext.Chapters.Update(chapter);
        }

        public void Delete(Chapter chapter)
        {
            _applicationUserDbContext.Chapters.Remove(chapter);
        }
    }
}
