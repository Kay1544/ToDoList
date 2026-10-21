using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TodoService
    {
        private readonly List<TodoModel> _tasks = new List<TodoModel>();
        private readonly ITaskRepository _repository;

        public TodoService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public IReadOnlyList<TodoModel> Tasks => _tasks;

        public void AddTask(TodoModel task)
        {
            if (task == null) throw new ArgumentNullException(nameof(task));
            _tasks.Add(task);
        }

        public void EditTask(Guid id, string newTitle, string newDescription,
                             DateTime newDueDate, TaskPriority newPriority)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return;

            task.Title = newTitle;
            task.Description = newDescription;
            task.DueDate = newDueDate;
            task.Priority = newPriority;
        }

        public void DeleteTask(Guid id)
        {
            _tasks.RemoveAll(t => t.Id == id);
        }

        public void ChangeStatus(Guid id, TaskStatus newStatus)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
                task.Status = newStatus;
        }

        public void SaveToFile() => _repository.Save(_tasks);

        public void LoadFromFile()
        {
            var loaded = _repository.Load();
            _tasks.Clear();
            _tasks.AddRange(loaded);
        }
    }
}