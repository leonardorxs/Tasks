using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tasks.Models;

namespace Tasks.Services
{
    public interface ITaskItemService
    {
        Task<IEnumerable<TaskItem>> GetItemsAsync(bool? filter, IdentityUser user);

        Task<bool> AddItemAsync(TaskItem newItem);

        Task<bool> DeleteItem(int? id);

        TaskItem GetItemById(int? id);

        Task Update(TaskItem item, IdentityUser user);

    }
}