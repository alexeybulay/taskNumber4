using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.EntityFrameworkCore;
using WebApp3.Areas.Identity.DbContext;
using WebApp3.Helpers;
using WebApp3.Interfaces;
using WebApp3.Models;
using WebApp3.ViewModels;

namespace WebApp3.Controllers
{
    public class FanficController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationUserDbContext _a;
        public FanficController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, ApplicationUserDbContext a)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _a = a;
        }
        // GET: FanficController
        // [Route("fanficlist/{fandomId?}")]
        [NoDirectAccessAttribute]
        public async Task<IActionResult> FanficList(int idFandom = 0, int idCategory = 0, int idTag = 0, string nameUser = null, int page = 1)
        {
            //var user =  _userManager.GetUserId(HttpContext.User);
            //var fanfics = _unitOfWork.Fanfic.GetMyFanfics(user);
            //var viewmodel = _mapper.Map<List<FanficViewModel>>(fanfics);
            //if (!string.IsNullOrEmpty(fanficName))
            //{
            //    return View(_unitOfWork.Fanfic.GerResultSearchByName(fanficName));
            //}

            //int pageSize = 1; // количество элементов на странице
            //var fanfics = _a.FanficsTags.Include(n => n.Fanfic)
            //    .ThenInclude(n => n.ApplicationUser)
            //    .Include(n => n.Fanfic.Fandoms)
            //    .Include(c => c.Fanfic.Category)
            //    .Include(n => n.Fanfic.Likes)
            //    .Include(n => n.Fanfic.Comments)
            //    .Include(n => n.Tag);
            //int countElement = await fanfics.CountAsync();
            //var items = await fanfics.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            //PaginationPageViewModel paginationPageViewModel = new PaginationPageViewModel(countElement, page, pageSize);
            //FanficTagViewModel fanficTagViewModel = new FanficTagViewModel()
            //{
            //    PaginationPageViewModel = paginationPageViewModel,
            //    FanficsTags = items
            //};
            if (idFandom != 0)
            {
                return View(await _unitOfWork.Fanfic.GerResultSearchByFandomIdAsync(idFandom));
            }  
            if (idCategory != 0)
            {
                return View(await _unitOfWork.Fanfic.GerResultSearchByCategoryIdAsync(idCategory));
            } 
            if (idTag != 0)
            {
                return View(await _unitOfWork.Fanfic.GerResultSearchByTagIdAsync(idTag));
            }

            if (!String.IsNullOrEmpty(nameUser))
            {
                return View(await _unitOfWork.Fanfic.GetMyFanficsAsync(nameUser));
            }


            return View(await _unitOfWork.Fanfic.GetAllAsync());
        }
        // GET: FanficController/Details/5
        // [Route("aboutfanfic/{id}")]
        [HttpGet]
        [ActionName("AboutFanfic")]
        public async Task<IActionResult> AboutFanficView(int id, int page = 1)
        {
            int pagesize = 12;
            var model = await _unitOfWork.Fanfic.GetByIdAsync(id); 
            var viewmodel = new FanficTagViewModel()
            {
                ApplicationUser = model.ApplicationUser,
                DateTimePublish = model.DateTimePublish,
                FanficId = model.FanficId,
                FanficsTags = model.FanficsTags,
                ApplicationUserId = model.ApplicationUserId,
                NameFanfic = model.NameFanfic,
                Description = model.Description
            };
            var comme = _a.Comments.Include(f => f.Fanfics)
                .Include(a => a.ApplicationUsers).Where(fi => fi.FanficId == id);
            int countElement = await comme.CountAsync();
            PaginationPageViewModel paginationPageViewModel = new PaginationPageViewModel(countElement, page, pagesize);
            var items = await comme.Skip((page -  1) * pagesize).Take(pagesize).ToListAsync();
            viewmodel.PaginationPageViewModel = paginationPageViewModel;
            viewmodel.Comments = items;
            return View(viewmodel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AboutFanfic(FanficTagViewModel fanficTagViewModel,int id)
        {
            try
            {
                var model = await _unitOfWork.Fanfic.GetByIdAsync(id);
                var user = _userManager.GetUserId(HttpContext.User);
                var userName = await _userManager.FindByEmailAsync(User.Identity.Name);
                #region LikeAction

                if (fanficTagViewModel.CommentContent == null)
                {
                    var like = new Like()
                    {
                        ApplicationUserId = user,
                        FanficId = model.FanficId
                    };
                    var modellike = _unitOfWork.Like.FindLike(model.FanficId, user);
                    if (modellike == null)
                    {
                        _unitOfWork.Like.AddLike(like);
                      await  _unitOfWork.SaveAsync();
                        return Json(new { isValid = true });
                    }

                    _unitOfWork.Like.RemoveLike(modellike);
                   await _unitOfWork.SaveAsync();
                    return Json(new { isValid = true});
                }
                #endregion
                //#region CommentAction

                else
                {
                    var comment = new Comment()
                    {
                        ApplicationUserId = user,
                        FanficId = model.FanficId,
                        CommentContent = fanficTagViewModel.CommentContent
                    };
                    _unitOfWork.Comment.AddComment(comment);
                    await _unitOfWork.SaveAsync();
                    return Json(new { isValid = true });
                }

                //#endregion
            }
            catch
            {
                return View("FanficList");
            }
        }
        // GET: FanficController/Create
        [NoDirectAccessAttribute]
        public async Task<IActionResult> CreateFanfic()
        {
        
                var tagsFromRepo = await _unitOfWork.Tag.GetAllAsync();
                var selectList = new List<SelectListItem>();
                
                foreach (var tags in tagsFromRepo) { selectList.Add(new SelectListItem(tags.TagTitle, tags.TagId.ToString())); }

                var createFanficViewModel = new CreateFanficViewModel()
                {
                    Fandoms = _a.Fandoms.ToList(),
                    Categories = _a.Categories.ToList(),
                    Tags = selectList
                };

                return View(createFanficViewModel);
     
        }

        // POST: FanficController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFanfic(CreateFanficViewModel createFanficViewModel)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                var model = new Fanfic
                {
                    NameFanfic = createFanficViewModel.NameFanfic,
                    Description = createFanficViewModel.Description,
                    DateTimePublish = createFanficViewModel.DateTimePublish,
                    ApplicationUser = user,
                    FandomId = createFanficViewModel.selectedFandom,
                    CategoryId = createFanficViewModel.selectedCategory
                };
                foreach (var tags in createFanficViewModel.SelectedTags)
                {
                    model.FanficsTags.Add(new FanficTag()
                    {
                        TagId = Int32.Parse(tags)
                    });
                }
                
               
                await _unitOfWork.Fanfic.InsertAsync(model);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("FanficList", "Fanfic");
            }
            catch
            {
                return View(nameof(FanficList));
            }
        }

        // GET: FanficController/Edit/5
        [NoDirectAccessAttribute]
        public async Task<IActionResult> Edit(int id)
        {
            var fanfic = await _unitOfWork.Fanfic.GetByIdAsync(id);
            var tags = await _unitOfWork.Tag.GetAllAsync();
            var selectedTags = fanfic.FanficsTags.Select(x => new Tag()
            {
                TagId = x.Tag.TagId,
                TagTitle = x.Tag.TagTitle
            });
            var selectList = new List<SelectListItem>();
            tags.ForEach(i => selectList
                .Add(new SelectListItem(
                    i.TagTitle,
                    i.TagId.ToString(),
                    selectedTags.Select(n => n.TagId)
                        .Contains(i.TagId)))
            );
            var viewmodel = new FanficEditViewModel()
            {
                FanficId = fanfic.FanficId,
                NameFanfic = fanfic.NameFanfic,
                Description = fanfic.Description,
                Tags = selectList
            };
            return View(viewmodel);
        }

        // POST: FanficController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FanficEditViewModel fanficEditViewModel, int id)
        {
            try
            {
                fanficEditViewModel.FanficId = id;
                var fanfic = await _unitOfWork.Fanfic.GetByIdAsync(fanficEditViewModel.FanficId);
                fanfic.NameFanfic = fanficEditViewModel.NameFanfic;
                fanfic.Description = fanficEditViewModel.Description;
                var selectedTags = fanficEditViewModel.SelectedTags;
                var resultTags = fanfic.FanficsTags.Select(n => n.TagId.ToString()).ToList();
                var addtags = selectedTags.Except(resultTags).ToList();
                var removetags = resultTags.Except(selectedTags).ToList();
                fanfic.FanficsTags = fanfic.FanficsTags
                    .Where(x => !removetags.Contains(x.TagId.ToString())).ToList();
                foreach (var item in addtags)
                {
                    fanfic.FanficsTags.Add(new FanficTag()
                    {
                        TagId = Int32.Parse(item),
                        FanficId = fanfic.FanficId
                    });
                }
                await _unitOfWork.SaveAsync();
                return Json(new{isValid = true});
            }
            catch
            {
                return View();
            }
        }

        // GET: FanficController/Delete
        // GET: FanficController/Delete/5
        [HttpGet]
        [NoDirectAccessAttribute]
        public async Task<IActionResult> Delete(int id)
        {
            var fanfic = await _unitOfWork.Fanfic.GetByIdAsync(id);
            var fanficDeleteViewModel = new FanficDeleteViewModel()
            {
                FanficId = fanfic.FanficId,
                NameFanfic = fanfic.NameFanfic,
                Description = fanfic.Description
            };
            return View(fanficDeleteViewModel);
        }

        // POST: FanficController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            try
            {
                var fanfic = await _unitOfWork.Fanfic.GetByIdAsync(id);
                _unitOfWork.Fanfic.Delete(fanfic);
                    await _unitOfWork.SaveAsync();
                    return Json(new
                        {isValid = true});
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [NoDirectAccessAttribute]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            var commentContent = await _unitOfWork.Comment.GetCommentById(commentId);
            var commentDeleteViewModel = new CommentDeleteViewModel()
            {
                CommentId = commentContent.CommentId,
                CommentContent = commentContent.CommentContent
            };
            return View(commentDeleteViewModel);
        }
        [HttpPost, ActionName("DeleteComment")]
        public async Task<IActionResult> ConfirmDeleteComment(int commentId)
        {
            var commentContent = await _unitOfWork.Comment.GetCommentById(commentId);
             _unitOfWork.Comment.RemoveComment(commentContent);
            await _unitOfWork.SaveAsync();
            return Json(new
                { isValid = true });
        }
    }
}
