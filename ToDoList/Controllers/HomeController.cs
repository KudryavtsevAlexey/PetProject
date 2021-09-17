using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoList.Entities;

namespace ToDoList.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> MakeTasks()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var tasks = _dbContext.TaskModels.Where(t=>user == t.ApplicationUser).ToList();
                
                return View(tasks);
            }

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult CreateTask()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskModel taskModel)
        {
            var user = await _userManager.GetUserAsync(User);
            taskModel.ApplicationUser = user;
            if (ModelState.IsValid)
            {
                _dbContext.TaskModels.Add(taskModel);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("MakeTasks", "Home");
            }
            return View(taskModel);
        }
        
        public async Task<IActionResult> MoreDetails(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id==null)
            {
                return NotFound();
            }

            var task = _dbContext.TaskModels.FirstOrDefault(t => id == t.TaskModelId);

            if (task==null)
            {
                return NotFound();
            }

            return View(task);
        } 

        public async Task<IActionResult> EditTask(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var task = _dbContext.TaskModels.FirstOrDefault(t => id == t.TaskModelId);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }
        [HttpPost]
        public async Task<IActionResult> EditTask(TaskModel taskModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                taskModel.EditedAt = DateTime.UtcNow;
                taskModel.IsEdited = true;
                _dbContext.TaskModels.Update(taskModel);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction("MakeTasks", "Home");
            }
            return View(taskModel);
        }

        public async Task<IActionResult> DeleteTask(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (id == null)
            {
                return NotFound();
            }

            var task = _dbContext.TaskModels.FirstOrDefault(t => t.TaskModelId == id);

            if (task == null)
            {
                return NotFound();
            }


            user.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("MakeTasks", "Home");
        }

        public async Task<IActionResult> FilterByDeadline()
        {
            var user = await _userManager.GetUserAsync(User);
            var filteredList = _dbContext.TaskModels.Where(t => t.ApplicationUser == user).OrderBy(d=>d.FinishBefore).ToList();
            return View("MakeTasks", filteredList);
        }

        public async Task<IActionResult> FilterByEditing()
        {
            var user = await _userManager.GetUserAsync(User);
            var filteredList = _dbContext.TaskModels.Where(t => t.ApplicationUser == user).OrderByDescending(d => d.IsEdited)
                .ToList();
            return View("MakeTasks", filteredList);
        }

        public async Task<IActionResult> FilterByTimeOfLastUpdate()
        {
            var user = await _userManager.GetUserAsync(User);
            var filteredList = _dbContext.TaskModels.Where(t=>t.ApplicationUser==user).OrderBy(d => d.EditedAt).ToList();
            return View("MakeTasks", filteredList);
        }

        public async Task<IActionResult> FilterByExecutionPriority()
        {
            var user = await _userManager.GetUserAsync(User);
            var filteredList = _dbContext.TaskModels.Where(t => t.ApplicationUser == user).OrderBy(d => d.ExecutionPriority).ToList();
            return View("MakeTasks", filteredList);
        }

        public async Task<IActionResult> Profile()
        {
            var users = _dbContext.Users.Include(t => t.Tasks).ToList();
            var user = await _userManager.GetUserAsync(User);
            return View(user);
        }
    }
}
