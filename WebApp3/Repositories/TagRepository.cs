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
    public class TagRepository : ITag
    {
        private readonly ApplicationUserDbContext _applicationUserDbContext;

        public TagRepository(ApplicationUserDbContext applicationUserDbContext)
        {
            _applicationUserDbContext = applicationUserDbContext;
        }

        public IQueryable<Tag> GetTagsAsync()
        {
            return _applicationUserDbContext.Tags.Include(p => p.FanficsTags)
                .ThenInclude(t => t.Fanfic);
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _applicationUserDbContext.Tags.ToListAsync();
        }

        public Tag GetById(int Id)
        {
            return _applicationUserDbContext.Tags.FirstOrDefault(x => x.TagId == Id);
        }

        public async Task InsertAsync(Tag tag)
        {
            await _applicationUserDbContext.Tags.AddAsync(tag);
        }

        public void Update(Tag tag)
        {
            _applicationUserDbContext.Tags.Update(tag);
        }

        public void Delete(Tag tag)
        {
            _applicationUserDbContext.Tags.Remove(tag);
        }
    }
}
