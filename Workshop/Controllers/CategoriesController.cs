using Microsoft.AspNetCore.Mvc;
using MVCWorkshop.Data.Entities;
using MVCWorkshop.Data.IRepos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCWorkshop.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepo _repo;

        public CategoriesController(ICategoryRepo repo)
        {
            _repo = repo;
        }
        public async Task<IReadOnlyList<Category>> Index()
        {
            return await _repo.ListAllAsync();
        }

        public async Task<IActionResult> AdminIndex()
        {
            return View(await _repo.ListAllAsync());
        }

        public  IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm]Category category)
        {
            await _repo.AddAsync(category);

            return RedirectToAction("AdminIndex");
        }

        public async Task<IActionResult> EditCategory(int Id)
        {
            return View(await _repo.GetByIdAsync(Id));
        }
        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            await _repo.UpdateAsync(category);
            return RedirectToAction("AdminIndex");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var entityToDelete = await _repo.GetByIdAsync(id);
            if (entityToDelete!=null)
            {
                _repo.Delete(entityToDelete);
                
            }
            return RedirectToAction("AdminIndex");
        }
    }
}
