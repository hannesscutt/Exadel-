namespace ExadelTimeTrackingSystem.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Helpers;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.WebAPI.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;
        private readonly IOptionsMonitor<TimeOutSettings> _options;

        public TasksController(ITaskService service, IOptionsMonitor<TimeOutSettings> options)
        {
            _service = service;
            _options = options;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> GetAllAsync()
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var tasks = await _service.GetAllAsync(cancellationToken);
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var task = await _service.GetByIdAsync(id, cancellationToken);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateAsync([FromBody] CreateTaskDTO task)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var taskDto = await _service.CreateAsync(task, cancellationToken);
            return Created(string.Empty, taskDto);
        }

        [HttpGet("on-date")]

        public async Task<ActionResult<List<TaskDTO>>> GetOnDateAsync([FromQuery, Required] DateTime date)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var tasks = await _service.GetOnDateAsync(date, cancellationToken);
            return Ok(tasks);
        }

        [HttpDelete("{id:guid}")]

        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            await _service.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<TaskDTO>> UpdateTaskAsync([FromBody] TaskDTO task)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var taskDto = await _service.UpdateAsync(task, cancellationToken);
            return Ok(taskDto);
        }

        [HttpPut("approve")]

        public async Task<ActionResult> ApproveAsync([FromQuery] DateTime date, [FromQuery] Guid projectId, [FromQuery] Guid employeeId)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            await _service.ApproveAsync(date, projectId, employeeId, cancellationToken);
            return NoContent();
        }

        [HttpPost("bulk")]

        public async Task<ActionResult<List<BulkCreateTaskDTO>>> BulkCreateAsync([FromBody] BulkCreateTaskDTO bulkCreateTaskDto)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var tasksDto = await _service.BulkCreateAsync(bulkCreateTaskDto, cancellationToken);
            return Created(string.Empty, tasksDto);
        }
    }
}