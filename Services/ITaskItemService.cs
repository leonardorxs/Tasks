using System.Collections.Generic;
using System.Threading.Tasks;
using Tasks.Models;

namespace Tasks.Services
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItem>> GetItemsAsync(bool? filter);

        Task<bool> AddItemAsync(TaskItem newItem);

        Task<bool> DeleteItem(int? id);

        TaskItem GetItemById(int? id);

        Task Update(TaskItem item);

    }
}