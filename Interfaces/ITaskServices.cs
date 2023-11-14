using System.Collections.Generic;
using Tasks.Models;
using Tasks.Interfaces;
using System.Linq;
using System.IO;
using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace Tasks.Interfaces
{
    public interface ITaskService
    {
        List<Task> GetAll();
        Task Get(int id);
        void Add(Task Task);
        void Delete(int id);
        void Update(Task Task);
        int Count {get;}
    }
}