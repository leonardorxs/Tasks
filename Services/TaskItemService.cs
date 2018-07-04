using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tasks.Data;
using Tasks.Models;

namespace Tasks.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ApplicationDbContext _context;

        public TaskItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetItemsAsync(bool? filter)
        {
            if (filter != null)
            {
                var items = await _context.Tasks
                            .Where(t => t.IsCompleted == filter)
                            .ToArrayAsync();
                return items;
            }
            else
            {
                var items = await _context.Tasks.ToArrayAsync();
                return items;
            }
        }

        public async Task<bool> AddItemAsync(TaskItem newItem)
        {
            var task = new TaskItem
            {
                IsCompleted = false,
                Name = newItem.Name,
                Deadline = newItem.Deadline
            };

            _context.Tasks.Add(task);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public async Task<bool> DeleteItem(int? id)
        {
            var foundItem = GetItemById(id);
            _context.Tasks.Remove(foundItem);
            var result = await _context.SaveChangesAsync();
            return result == 1;
        }

        public TaskItem GetItemById(int? id)
        {
            return _context.Tasks.Find(id);
        }

        public async Task Update(TaskItem item)
        {
            if (item == null)
                throw new ArgumentException(nameof(item));

            _context.Tasks.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}