using Microsoft.AspNetCore.Mvc;
using PetProject.Data;
using PetProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext = null;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MakeTasks()
        {
            var tasks = _dbContext.TaskModels.ToList();
            return View(tasks);
        }
        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTask(TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                _dbContext.TaskModels.Add(taskModel);
                _dbContext.SaveChanges();
                return RedirectToAction("MakeTasks", "Home");
            }
            return View(taskModel);
        }
        
        public IActionResult MoreDetails(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var task = _dbContext.TaskModels.FirstOrDefault(t => t.TaskModelId == id);

            if (task==null)
            {
                return NotFound();
            }

            return View(task);
        } 
    }
}
