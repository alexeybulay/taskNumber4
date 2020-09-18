using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Interfaces
{
    public interface IFanfic
    {
        IQueryable<FanficTagViewModel> GetFanficForPagination();
        Task<FanficTagViewModel> GetAllAsync();
        FanficTagViewModel GetTop10FanficsAsync();
        Task <FanficTagViewModel> GetMyFanficsAsync(string userName);
        Task<Fanfic> GetByIdAsync(int Id);
        FanficTagViewModel GerResultSearchByName(string nameFanfic);
        Task<FanficTagViewModel> GerResultSearchByFandomIdAsync(int idFandom);
        Task<FanficTagViewModel> GerResultSearchByCategoryIdAsync(int idCategory);
        Task<FanficTagViewModel> GerResultSearchByTagIdAsync(int idTag);
        Task InsertAsync(Fanfic fanfic);
        void Update(Fanfic fanfic);
        void Delete(Fanfic fanfic);
    }
}
