using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApp3.Controllers;
using WebApp3.Interfaces;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Helpers
{
    public class Helper : Profile
    {
        public Helper()
        {
            CreateMap<Tag, TagViewModel>().ReverseMap();
            CreateMap<CreateTagViewModel, Tag>();
            CreateMap<Fanfic, FanficViewModel>();
            CreateMap<Fanfic, FanficTagViewModel>();
            CreateMap<Fanfic, FanficDeleteViewModel>().ReverseMap();
            CreateMap<CreateFanficViewModel, Fanfic>();
            CreateMap<Chapter, CreateChapterViewModel>();

        }
    }
}
