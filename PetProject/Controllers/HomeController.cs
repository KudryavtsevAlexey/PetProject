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

        public IActionResult EditTask(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = _dbContext.TaskModels.FirstOrDefault(t => t.TaskModelId == id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        [HttpPost]
        public IActionResult EditTask(TaskModel taskModel)
        {
            if (ModelState.IsValid)
            {
                taskModel.EditedAt = DateTime.UtcNow;
                taskModel.IsEdited = true;
                _dbContext.TaskModels.Update(taskModel);
                _dbContext.SaveChanges();
                return RedirectToAction("MakeTasks", "Home");
            }
            return View(taskModel);
        }

        public IActionResult DeleteTask(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = _dbContext.TaskModels.FirstOrDefault(t => t.TaskModelId == id);

            if (task == null)
            {
                return NotFound();
            }

            _dbContext.TaskModels.Remove(task);
            _dbContext.SaveChanges();
            return RedirectToAction("MakeTasks", "Home");
        }

        public IActionResult FilterByDeadline()
        {
            var filteredlist = _dbContext.TaskModels.OrderBy(d=>d.FinishBefore).ToList();
            return View("MakeTasks", filteredlist);
        }

        public IActionResult FilterByEditing()
        {
            var filteredlist = _dbContext.TaskModels.OrderByDescending(d => d.IsEdited).ToList();
            return View("MakeTasks", filteredlist);
        }

        public IActionResult FilterByTimeOfLastUpdate()
        {
            var filteredlist = _dbContext.TaskModels.OrderBy(d => d.EditedAt).ToList();
            return View("MakeTasks", filteredlist);
        }

        public IActionResult FilterByExecutionPriority()
        {
            var filteredlist = _dbContext.TaskModels.OrderBy(d => d.ExecutionPriority).ToList();
            return View("MakeTasks", filteredlist);
        }
    }
}
