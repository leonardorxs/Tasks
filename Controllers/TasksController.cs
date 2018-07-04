using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasks.Models;
using Tasks.Services;
using Tasks.ViewModels;

namespace Tasks.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskItemService _taskService;

        public TasksController(ITaskItemService taskService)
        {
            _taskService = taskService;
        }
        //lista de tarefas
        public async Task<IActionResult> Index(bool? filter)
        {
            //obter itens da tarefa
            // TempTaskItemService service = new TempTaskItemService();
            // var tasks = service.GetItemAsync();

            var tasks = await _taskService.GetItemsAsync(filter);

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
            if (!ModelState.IsValid)
                return View(taskItem);

            await _taskService.AddItemAsync(taskItem);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var taskItem = _taskService.GetItemById(id);
            if (taskItem == null)
                return NotFound();

            return View(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
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

            if (!ModelState.IsValid)
                return View(taskItem);

            try
            {
                await _taskService.Update(taskItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(Index));

        }
    }
}