
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Tasks.Interfaces;
using Tasks.Models;

namespace Tasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        ITaskService TaskService;
        public TaskController(ITaskService taskService)
        {
            this.TaskService=taskService;
        }

        
       [HttpGet]
         public  ActionResult<List<Task>> GetAll() =>
        TaskService.GetAll();


        [HttpGet ("{id}")]
        public ActionResult<Task> Get(int id) 
        {   
           var Task = TaskService.Get(id);

            if (Task == null)
                return NotFound();

            return Task;
        }  
        
        [HttpPost] 
        public IActionResult Create(Task Task)
        {
            TaskService.Add(Task);
            return CreatedAtAction(nameof(Create), new {id=Task.Id}, Task);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Task Task)
        {
            if (id != Task.Id)
                return BadRequest();

            var existingTask = TaskService.Get(id);
            if (existingTask is null)
                return  NotFound();

            TaskService.Update(Task);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var Task = TaskService.Get(id);
            if (Task is null)
                return  NotFound();

            TaskService.Delete(id);

            return Content(TaskService.Count.ToString());
        }      
    }
}
