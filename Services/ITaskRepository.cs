using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Services
{
    public interface ITaskRepository
    {
        void Save(IEnumerable<TodoModel> tasks);
        List<TodoModel> Load();
    }
}