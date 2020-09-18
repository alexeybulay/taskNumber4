using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Interfaces;
using WebApp3.ViewModels;

namespace WebApp3.Repositories
{
    public class FandomRepository : IFandom
    {
        private ApplicationUserDbContext _applicationUserDbContext;

        public FandomRepository(ApplicationUserDbContext applicationUserDbContext) =>
            _applicationUserDbContext = applicationUserDbContext;
        public async Task<FanficTagViewModel> GetAllAsync()
        {
            return new FanficTagViewModel()
            {
                Fandoms = await _applicationUserDbContext.Fandoms.ToListAsync()
            };
        }
    }
}
