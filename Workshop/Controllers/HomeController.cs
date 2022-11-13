using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCWorkshop.Data.IRepos;
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

        public HomeController(INewsRepo repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _repo.ListAllAsync());
        }

        
    }
}
