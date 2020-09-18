using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Interfaces;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Controllers
{
    public class ChapterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUserDbContext _a;
        public ChapterController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, ApplicationUserDbContext a)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _a = a;
        }
        // GET: ChapterController
        public ActionResult Index(int id)
        {
            return View(new CreateChapterViewModel()
            {
                Chapters = _a.Chapters.Include(n => n.Fanfics)
                    .ToList()
                    .Where(n => n.FanficId == id)
            });
        }

        // GET: ChapterController/Details/5
        public ActionResult Details(int id)
        {
            var chapter = _unitOfWork.Chapter.GetChapterById(id);
            var viewmodel = new CreateChapterViewModel
            {
                NameChapter = chapter.NameChapter, TextChapter = chapter.TextChapter
            };
            return View(viewmodel);
        }

        // GET: ChapterController/Create
        [ActionName("Create")]
        public async Task<IActionResult> CreateChapter(int id)
        {
            var fanfic = await _unitOfWork.Fanfic.GetByIdAsync(id);
            var viewmodel = new CreateChapterViewModel()
            {
                Fanfics = fanfic
            };
            return View(viewmodel);
        }

        // POST: ChapterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,CreateChapterViewModel chapterViewModel)
        {
            try
            {
                var fanfic = await _unitOfWork.Fanfic.GetByIdAsync(id);
                var user = fanfic.ApplicationUser;
                var chapter = new Chapter()
                {
                    NameChapter = chapterViewModel.NameChapter,
                    TextChapter = chapterViewModel.TextChapter,
                    Fanfics = fanfic,
                    ApplicationUser = user
                };
                _unitOfWork.Chapter.AddChapter(chapter);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("FanficList", "Fanfic");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: ChapterController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _unitOfWork.Chapter.GetChapterById(id);
            var viewmodel = new EditChapter()
            {
                NameChapter = model.NameChapter,
                TextChapter = model.TextChapter
            };
            return View(viewmodel);
        }

        // POST: ChapterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChapterController/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: ChapterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
