using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWorkshop.Data.Entities;
using MVCWorkshop.Data.IRepos;
using MVCWorkshop.Models.ViewModels;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

namespace MVCWorkshop.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepo _repo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public NewsController(INewsRepo repo, ICategoryRepo categoryRepo, IWebHostEnvironment hostingEnvironment)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AdminIndex()
        {
            return View(await _repo.ListAllAsync());
        }

        public IActionResult CreateNews()
        {
            var model = new CreateNewsVM() { Items = _categoryRepo.GetListItems() };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateNews(CreateNewsVM newsVM)
        {
            var entityToCreate = new Data.Entities.News()
            {
                Body = newsVM.Body,
                CategoryId = newsVM.CategoryId,
                Title = newsVM.Title,
            };
            if (newsVM.Image!= null)
            {
                var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath,"Images");
                var fileName = Guid.NewGuid().ToString() + "_" + newsVM.Image.FileName;
                var filePath = Path.Combine(uploadFolder, fileName);
                newsVM.Image.CopyTo(new FileStream(filePath,FileMode.Create));
                entityToCreate.ImageUrl = fileName;
            }
            await _repo.AddAsync(entityToCreate);
            return RedirectToAction("AdminIndex");
        }
        public async Task<IActionResult> EditNews(int Id)
        {
            var News = await _repo.GetByIdAsync(Id);
            var model = new EditNewsVM()
            {
                Id = News.Id,
                Title = News.Title,
                Body = News.Body,
                CategoryId = News.CategoryId,
                Items = _categoryRepo.GetListItems()
            };
            

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditNews(EditNewsVM model)
        {
            var entityToEdit= await _repo.GetByIdAsync(model.Id);
            if (entityToEdit != null)
            {
                entityToEdit.Title = model.Title;
                entityToEdit.Body = model.Body;
                entityToEdit.CategoryId = model.CategoryId;

                if (model.Image != null)
                {
                    var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Images");
                    var fileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    var filePath = Path.Combine(uploadFolder, fileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                    entityToEdit.ImageUrl = fileName;
                }
                await _repo.UpdateAsync(entityToEdit);
            }

            return RedirectToAction("Details",new { Id=model.Id});
        }

        public async Task<IActionResult> Details(int Id)
        {
            ViewData["isAdmin"] = true;
            ViewData["BaseURL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var News = await _repo.GetByIdAsync(Id);
            News.Category = await _categoryRepo.GetByIdAsync(News.CategoryId);
            return View(News);

        }
        public async Task<IActionResult> NewsDetails(int Id)
        {
            ViewData["isAdmin"] = false;
            ViewData["BaseURL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var News = await _repo.GetByIdAsync(Id);
            News.Category = await _categoryRepo.GetByIdAsync(News.CategoryId);
            return View("Details",News);

        }
        [HttpPost]
        public async Task<IActionResult> DeleteNews(int Id)
        {
            var EntityToDelete = await _repo.GetByIdAsync(Id);
            if (EntityToDelete !=null)
            {
                _repo.Delete(EntityToDelete);

            }
            return RedirectToAction("AdminIndex");
        }
    }
}
