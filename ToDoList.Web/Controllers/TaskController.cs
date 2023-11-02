﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Filters.Task;
using ToDoList.Domain.Utils;
using ToDoList.Domain.ViewModels.Task;
using ToDoList.Services.Interfaces;

namespace ToDoList.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<IActionResult> GetCompletedTasks()
        {
            var result = await _taskService.GetCompletedTasks();

            return Json(result);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CalculateCompletedTasks()
        {
            var response = await _taskService.CalculateCompletedTasks();

            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var csvService = new CSVBaseService<TaskViewModel>();
                var uploadFile = csvService.UploadFile(response.Data);

                return File(uploadFile, "text/csv", $"Статистика за {DateTime.Now.ToLongDateString()}.csv");
            }

            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            var response = await _taskService.Create(model);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> EndTask(long id)
        {
            var response = await _taskService.EndTask(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> TaskHandler(TaskFilter filter)
        {
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;

            filter.Skip = skip;
            filter.PageSize = pageSize;

            var response = await _taskService.GetTasks(filter);

            return Json(new { recordsFiltered = response.Total, recordsTotal = response.Total, data = response.Data }); ;
        }
    }
}