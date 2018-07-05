using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasks.Models;
using Tasks.Services;
using Tasks.ViewModels;

namespace Tasks.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ITaskItemService _taskService;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ITaskItemService taskService, UserManager<IdentityUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        //lista de tarefas
        public async Task<IActionResult> Index(bool? filter)
        {
            //obter itens da tarefa
            // TempTaskItemService service = new TempTaskItemService();
            // var tasks = service.GetItemAsync();
            var currentUser = await _userManager.GetUserAsync(User);
            // if (currentUser == null)
            //     return Challenge();

            var tasks = await _taskService.GetItemsAsync(filter, currentUser);

            var model = new TaskViewModel()
            {
                TaskItems = tasks
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddTaskItem()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTaskItem([Bind("Id, IsCompleted, Name, Deadline")] TaskItem taskItem)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            taskItem.OwnerId = currentUser.Id;
            if (!ModelState.IsValid)
                return View(taskItem);

            await _taskService.AddItemAsync(taskItem);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var taskItem = _taskService.GetItemById(id);
            if (taskItem == null)
                return NotFound();

            return View(taskItem);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskService.DeleteItem(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var taskItem = _taskService.GetItemById(id);
            if (taskItem == null)
                return NotFound();

            return View(taskItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, IsCompleted, Deadline")] TaskItem taskItem)
        {
            if (id != taskItem.Id)
                return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
                return View(taskItem);

            try
            {
                await _taskService.Update(taskItem, currentUser);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));

        }
    }
}