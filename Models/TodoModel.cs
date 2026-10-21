using System;

namespace ToDoList.Models
{
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }

    public class TodoModel
    {
        private string _title;
        private string _description;

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название не может быть пустым");
                _title = value;
            }
        }

        public string Description
        {
            get => _description;
            set => _description = value ?? string.Empty;
        }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1);
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;

        public TodoModel() { }

        public TodoModel(string title, string description, DateTime dueDate,
                         TaskPriority priority = TaskPriority.Medium)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
        }
    }
}