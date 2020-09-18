using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Interfaces;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Repositories
{
    public class FanficRepository : IFanfic
    {
        private readonly ApplicationUserDbContext _applicationUserDbContext;

        public FanficRepository(ApplicationUserDbContext applicationUserDbContext)
        {
            _applicationUserDbContext = applicationUserDbContext;
        }


        public IQueryable<FanficTagViewModel> GetFanficForPagination()
        {
            throw new NotImplementedException();
        }

        public async Task<FanficTagViewModel> GetAllAsync()
        {
            return new FanficTagViewModel()
            {
                FanficsTags = await _applicationUserDbContext.FanficsTags
                    .Include(f => f.Fanfic)
                    .ThenInclude(n => n.ApplicationUser)
                    .Include(n => n.Fanfic.Fandoms)
                    .Include(c => c.Fanfic.Category)
                    .Include(n => n.Fanfic.Likes)
                    .Include(n => n.Fanfic.Comments)
                    .Include(ft => ft.Fanfic.FanficsTags)
                    .ThenInclude(t => t.Tag)
                    .ToListAsync()
            };
        }

        public FanficTagViewModel GetTop10FanficsAsync()
        {
            return new FanficTagViewModel()
            {
                FanficsTags = _applicationUserDbContext.FanficsTags
                    .Include(f => f.Fanfic)
                    .ThenInclude(n => n.ApplicationUser)
                    .Include(n => n.Fanfic.Fandoms)
                    .Include(c => c.Fanfic.Category)
                    .Include(n => n.Fanfic.Likes)
                    .Include(n => n.Fanfic.Comments)
                    .Include(ft => ft.Fanfic.FanficsTags)
                    .ThenInclude(t => t.Tag)
                    .OrderByDescending(like => like.Fanfic.Likes)

            };
        }

        public async Task<FanficTagViewModel> GetMyFanficsAsync(string userName)
        {
            return new FanficTagViewModel()
            {
                FanficsTags = await _applicationUserDbContext.FanficsTags
                    .Include(p => p.Fanfic.ApplicationUser)
                    .Include(n => n.Fanfic.Likes)
                    .Include(n => n.Fanfic.Comments)
                    .Include(n => n.Fanfic.Fandoms)
                    .Include(n => n.Fanfic.Category)
                    .Include(n => n.Tag)
                    .Where(i => i.Fanfic.ApplicationUser.Email == userName)
                    .ToListAsync()
            };
        }

        public async Task<Fanfic> GetByIdAsync(int Id)
        {
            return await _applicationUserDbContext.Fanfics
                .Include(n => n.ApplicationUser)
                .Include(n => n.Likes)
                .Include(n => n.Comments)
                .Include(n => n.Fandoms)
                .Include(n => n.Category)
                .Include(n => n.FanficsTags)
                .ThenInclude(n => n.Tag)
                .FirstOrDefaultAsync(x => x.FanficId == Id);
        }

        public FanficTagViewModel GerResultSearchByName(string nameFanfic)
        {
            return new FanficTagViewModel()
            {
                FanficsTags = _applicationUserDbContext.FanficsTags.Include(p => p.Fanfic)
                    .ThenInclude(f => f.ApplicationUser)
                    .Include(n => n.Fanfic.Likes)
                    .Include(n => n.Fanfic.Comments)
                    .Include(n => n.Fanfic.Fandoms)
                    .Include(n => n.Fanfic.Category)
                    .Include(n => n.Fanfic.FanficsTags)
                    .ThenInclude(n => n.Tag)
                    .Where(n => n.Fanfic.NameFanfic.Contains(nameFanfic))
                    .ToList(),
            };
        }

        public async Task<FanficTagViewModel> GerResultSearchByFandomIdAsync(int idFandom)
        {
            return new FanficTagViewModel()
            {
                FanficsTags = await _applicationUserDbContext.FanficsTags.Include(n => n.Fanfic)
                    .ThenInclude(a => a.ApplicationUser)
                    .Include(f => f.Fanfic.Fandoms)
                    .Include(c => c.Fanfic.Category)
                    .Include(n => n.Fanfic.Likes)
                    .Include(n => n.Fanfic.Comments)
                    .Include(n => n.Tag)
                    .Where(n => n.Fanfic.FandomId == idFandom)
                    .ToListAsync()
            };
        }

        public async Task<FanficTagViewModel> GerResultSearchByCategoryIdAsync(int idCategory)
        {
            return new FanficTagViewModel()
            {
                FanficsTags = await _applicationUserDbContext.FanficsTags.Include(n => n.Fanfic)
                    .ThenInclude(a => a.ApplicationUser)
                    .Include(f => f.Fanfic.Fandoms)
                    .Include(c => c.Fanfic.Category)
                    .Include(n => n.Fanfic.Likes)
                    .Include(n => n.Fanfic.Comments)
                    .Include(n => n.Tag)
                    .Where(n => n.Fanfic.CategoryId == idCategory)
                    .ToListAsync()
            };
        }

        public async Task<FanficTagViewModel> GerResultSearchByTagIdAsync(int idTag)
        {
            return new FanficTagViewModel()
            {
                FanficsTags = await _applicationUserDbContext.FanficsTags.Include(n => n.Fanfic)
                    .ThenInclude(a => a.ApplicationUser)
                    .Include(f => f.Fanfic.Fandoms)
                    .Include(c => c.Fanfic.Category)
                    .Include(n => n.Fanfic.Likes)
                    .Include(n => n.Fanfic.Comments)
                    .Include(n => n.Fanfic.FanficsTags)
                    .ThenInclude(t => t.Tag)
                    .Where(t => t.TagId == idTag)
                    .Distinct()
                    .ToListAsync()
            };
        }

        public async Task InsertAsync(Fanfic fanfic)
        {
           await _applicationUserDbContext.Fanfics.AddAsync(fanfic);
        }

        public void Update(Fanfic fanfic)
        {
            _applicationUserDbContext.Fanfics.Update(fanfic);
        }

        public void Delete(Fanfic fanfic)
        {
            _applicationUserDbContext.Fanfics.Remove(fanfic);
        }
    }
}
