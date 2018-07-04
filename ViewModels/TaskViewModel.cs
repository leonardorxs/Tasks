using System.Collections.Generic;
using Tasks.Models;

namespace Tasks.ViewModels
{
    public class TaskViewModel
    {
        public IEnumerable<TaskItem> TaskItems { get; set; }
    }
}