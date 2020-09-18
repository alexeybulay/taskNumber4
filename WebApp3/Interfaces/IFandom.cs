using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Interfaces
{
    public interface IFandom
    {
        Task<FanficTagViewModel> GetAllAsync();
    }
}
