using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class JsonTaskRepository : ITaskRepository
    {
        private readonly string _filePath;

        public JsonTaskRepository(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(IEnumerable<TodoModel> tasks)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(_filePath, json);
        }

        public List<TodoModel> Load()
        {
            if (!File.Exists(_filePath))
                return new List<TodoModel>();

            string json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<TodoModel>();

            return JsonSerializer.Deserialize<List<TodoModel>>(json)
                   ?? new List<TodoModel>();
        }
    }
}