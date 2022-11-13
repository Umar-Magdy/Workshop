using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCWorkshop.Data.IRepos;
using MVCWorkshop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Models;

namespace Workshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsRepo _repo;
        private readonly ICategoryRepo _categoryRepo;

        public HomeController(INewsRepo repo,ICategoryRepo categoryRepo)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeVM() { News = await _repo.ListAllAsync(x => x.Category), Items = _categoryRepo.GetListItems() };
            return View(model);
        }

        public async Task<IActionResult> Filter(HomeVM model)
        {
            var items = _categoryRepo.GetListItems();
            model.News = await _repo.ListOrderedAsync(x => true, x => x.CategoryId == model.selectedCategory, 0, int.MaxValue, x => x.Category);
            model.Items = _categoryRepo.GetListItems();
            return View("Index",model);
        }



    }
}
