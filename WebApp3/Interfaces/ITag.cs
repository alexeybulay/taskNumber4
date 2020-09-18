using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;

namespace WebApp3.Interfaces
{
    public interface ITag
    {
        IQueryable<Tag> GetTagsAsync();
        Task<List<Tag>> GetAllAsync();
        Tag GetById(int Id);
        Task InsertAsync(Tag tag);
        void Update(Tag tag);
        void Delete(Tag tag);
    }
}
