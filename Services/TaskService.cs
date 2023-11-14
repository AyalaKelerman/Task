using Tasks.Interfaces;
using Tasks.Models;

using System.Linq;
using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;

namespace Tasks.Services
{
    public class TaskService : ITaskService
    {
        List<Task> Tasks { get; }
        private IWebHostEnvironment  webHost;
        private string filePath;
        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "Task.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                Tasks = JsonSerializer.Deserialize<List<Task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
    
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(Tasks));
        }
        public List<Task> GetAll() => Tasks;

        public Task Get(int id) => Tasks.FirstOrDefault(p => p.Id == id);

        public void Add(Task Task)
        {
            Task.Id = Tasks.Count() + 1;
            Tasks.Add(Task);
            saveToFile();
        }

        public void Delete(int id)
        {
            var Task = Get(id);
            if (Task is null)
                return;

            Tasks.Remove(Task);
            saveToFile();
        }

        public void Update(Task Task)
        {
            var index = Tasks.FindIndex(p => p.Id == Task.Id);
            if (index == -1)
                return;

            Tasks[index] = Task;
            saveToFile();
        }

        public int Count => Tasks.Count();
    }
}
