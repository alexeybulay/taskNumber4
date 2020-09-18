using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Helpers;
using WebApp3.Interfaces;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Controllers
{
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationUserDbContext _applicationUserDbContext;
        private IMapper _mapper;
        public TagController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationUserDbContext applicationUserDbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _applicationUserDbContext = applicationUserDbContext;
        }
        // GET: TagController
        public async Task<IActionResult> TagList(int page = 1)
        {
            int pageSize = 12; // количество элементов на странице
            IQueryable<Tag> tag = _unitOfWork.Tag.GetTagsAsync();
            int countElement = await tag.CountAsync();
            var items = await tag.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PaginationPageViewModel paginationPageViewModel = new PaginationPageViewModel(countElement, page, pageSize);
            FanficTagViewModel fanficTagViewModel = new FanficTagViewModel()
            {
                PaginationPageViewModel = paginationPageViewModel,
                Tags = items
            };
         
                return View(fanficTagViewModel);
         

        }

        // GET: TagController/Details/5
     

        // GET: TagController/Create
        public ActionResult CreateTag()
        {
            return View();
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTag(CreateTagViewModel createTagViewModel)
        {
            try
            {
                IEnumerable<Tag> tagsList = await _unitOfWork.Tag.GetAllAsync();
                var model = _mapper.Map<Tag>(createTagViewModel);
                if (ModelState.IsValid)
                {
                    if (String.IsNullOrEmpty(model.TagTitle) || String.IsNullOrEmpty(model.TagDescription))
                    {
                        ModelState.AddModelError("", "");
                        return Json(new
                            {isValid = false, textMessage = "Значение не может быть пустым",  html = HelperForJSON.RenderRazorViewToString(this, "CreateTag", createTagViewModel)});
                    }

                    foreach (var tagLists in tagsList)
                    {
                        if (model.TagTitle == tagLists.TagTitle)
                        {
                            ModelState.AddModelError("", "");
                            return Json(new
                                { isValid = false, textMessage= "Недопустимы названия одинаковых меток", html = HelperForJSON.RenderRazorViewToString(this, "CreateTag", createTagViewModel) });
                        }
                    }
                    
                 await _unitOfWork.Tag.InsertAsync(model);
                 await  _unitOfWork.SaveAsync();

                }
                 return Json(new {isValid = true });
            }
            catch
            {
                return View();
            }
        }

        //// GET: TagController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: TagController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: TagController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: TagController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
