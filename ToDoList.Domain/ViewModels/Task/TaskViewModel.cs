﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.ViewModels.Task
{
    public class TaskViewModel
    {
        public long Id { get; set; }
        [Display(Name= "Название")]
        public string Name { get; set; }
        [Display(Name = "Готовность")]
        public string isDone { get; set; }
        [Display(Name = "Приоритет")]
        public string Priority { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Дата создания")]
        public string Created { get; set; }
    }
}
