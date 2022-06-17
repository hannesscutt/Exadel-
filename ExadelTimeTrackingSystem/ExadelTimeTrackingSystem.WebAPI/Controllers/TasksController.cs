namespace ExadelTimeTrackingSystem.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> GetAllAsync()
        {
            var tasks = await _service.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            var task = await _service.GetByIdAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateAsync([FromBody] CreateTaskDTO task)
        {
            var taskDto = await _service.CreateAsync(task);
            return Created(string.Empty, taskDto);
        }

        [HttpGet("on-date")]

        public async Task<ActionResult<List<TaskDTO>>> GetOnDateAsync([FromQuery, Required] DateTime date)
        {
            var tasks = await _service.GetOnDateAsync(date);
            return Ok(tasks);
        }

        [HttpDelete("{id:guid}")]

        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<TaskDTO>> UpdateTaskAsync([FromBody] TaskDTO task)
        {
            var taskDto = await _service.UpdateAsync(task);
            return Ok(taskDto);
        }

        [HttpPut("approve")]

        public async Task<ActionResult> ApproveAsync([FromQuery] DateTime date, [FromQuery] Guid projectId, [FromQuery] Guid employeeId)
        {
            await _service.ApproveAsync(date, projectId, employeeId);
            return NoContent();
        }

        [HttpPost("bulk")]

        public async Task<ActionResult<List<BulkCreateTaskDTO>>> BulkCreateAsync([FromBody] BulkCreateTaskDTO bulkCreateTaskDto)
        {
            var tasksDto = await _service.BulkCreateAsync(bulkCreateTaskDto);
            return Created(string.Empty, tasksDto);
        }
    }
}