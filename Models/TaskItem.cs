using System;
using System.ComponentModel.DataAnnotations;

namespace Tasks.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string OwnerId { get; set; }

        [Required(ErrorMessage = "O nome da tarefa é obrigatório!", AllowEmptyStrings = false)]
        [Display(Name = "Nome da tarefa")]
        [StringLength(100, ErrorMessage = "O nome da tarefa deve ter, no máximo, 120 caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Tarefa completa?")]
        public bool IsCompleted { get; set; }


        [Display(Name = "Data limite")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTimeOffset? Deadline { get; set; }
    }
}