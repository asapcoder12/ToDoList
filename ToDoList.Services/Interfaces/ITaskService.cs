using Domain.Entity;
using ToDoList.Domain.Response;
using ToDoList.Domain.ViewModels.Task;

namespace ToDoList.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IBaseResponse<TaskEntity>> Create(CreateTaskViewModel model);
    }
}
