using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> EditNewsAsync(int Id)
        {
            var News = await _repo.GetByIdAsync(Id);
            var model = new EditNewsVM()
            {
                Id = News.Id,
                Title = News.Title,
                Body = News.Body,
                CategoryId = News.CategoryId
            };
            

            return View();
        }

        public async Task<IActionResult> Details(int Id)
        {
            ViewData["BaseURL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            var News = await _repo.GetByIdAsync(Id);
            News.Category = await _categoryRepo.GetByIdAsync(News.CategoryId);
            return View(News);

        }
    }
}
