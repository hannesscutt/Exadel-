namespace ExadelTimeTrackingSystem.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.WebAPI.Infrastructure;
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
            var token = ConfigureCancellationToken.Configure();
            var tasks = await _service.GetAllAsync(token);
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            var token = ConfigureCancellationToken.Configure();
            var task = await _service.GetByIdAsync(id, token);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateAsync([FromBody] CreateTaskDTO task)
        {
            var token = ConfigureCancellationToken.Configure();
            var taskDto = await _service.CreateAsync(task, token);
            return Created(string.Empty, taskDto);
        }

        [HttpGet("on-date")]

        public async Task<ActionResult<List<TaskDTO>>> GetOnDateAsync([FromQuery, Required] DateTime date)
        {
            var token = ConfigureCancellationToken.Configure();
            var tasks = await _service.GetOnDateAsync(date, token);
            return Ok(tasks);
        }

        [HttpDelete("{id:guid}")]

        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var token = ConfigureCancellationToken.Configure();
            await _service.DeleteAsync(id, token);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<TaskDTO>> UpdateTaskAsync([FromBody] TaskDTO task)
        {
            var token = ConfigureCancellationToken.Configure();
            var taskDto = await _service.UpdateAsync(task, token);
            return Ok(taskDto);
        }

        [HttpPut("approve")]

        public async Task<ActionResult> ApproveAsync([FromQuery] DateTime date, [FromQuery] Guid projectId, [FromQuery] Guid employeeId)
        {
            var token = ConfigureCancellationToken.Configure();
            await _service.ApproveAsync(date, projectId, employeeId, token);
            return NoContent();
        }

        [HttpPost("bulk")]

        public async Task<ActionResult<List<BulkCreateTaskDTO>>> BulkCreateAsync([FromBody] BulkCreateTaskDTO bulkCreateTaskDto)
        {
            var token = ConfigureCancellationToken.Configure();
            var tasksDto = await _service.BulkCreateAsync(bulkCreateTaskDto, token);
            return Created(string.Empty, tasksDto);
        }
    }
}