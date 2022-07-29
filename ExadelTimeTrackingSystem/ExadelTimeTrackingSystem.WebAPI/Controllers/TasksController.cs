namespace ExadelTimeTrackingSystem.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;
    using EmailService;
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
        private readonly ITaskService _taskService;
        private readonly IOptionsMonitor<TimeOutSettings> _options;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;

        public TasksController(ITaskService taskService, IOptionsMonitor<TimeOutSettings> options, IEmailSender emailSender, IUserService userService)
        {
            _taskService = taskService;
            _options = options;
            _emailSender = emailSender;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDTO>>> GetAllAsync()
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var tasks = await _taskService.GetAllAsync(cancellationToken);
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var task = await _taskService.GetByIdAsync(id, cancellationToken);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateAsync([FromBody] CreateTaskDTO task)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var taskDto = await _taskService.CreateAsync(task, cancellationToken);
            return Created(string.Empty, taskDto);
        }

        [HttpGet("on-date")]

        public async Task<ActionResult<List<TaskDTO>>> GetOnDateAsync([FromQuery, Required] DateTime date)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var tasks = await _taskService.GetOnDateAsync(date, cancellationToken);
            return Ok(tasks);
        }

        [HttpDelete("{id:guid}")]

        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            await _taskService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<TaskDTO>> UpdateTaskAsync([FromBody] UpdateTaskDTO task)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var taskDto = await _taskService.UpdateAsync(task, cancellationToken);
            return Ok(taskDto);
        }

        [HttpPut("approve")]

        public async Task<ActionResult> ApproveAsync([FromQuery] ApproveTaskDTO approveTaskDto)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            await _taskService.ApproveAsync(approveTaskDto.Date, approveTaskDto.ProjectId, approveTaskDto.EmployeeID, cancellationToken);
            return NoContent();
        }

        [HttpPost("bulk")]

        public async Task<ActionResult<List<BulkCreateTaskDTO>>> BulkCreateAsync([FromBody] BulkCreateTaskDTO bulkCreateTaskDto)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            var tasksDto = await _taskService.BulkCreateAsync(bulkCreateTaskDto, cancellationToken);
            return Created(string.Empty, tasksDto);
        }

        [HttpGet("emailtest")]
        public async Task<ActionResult> SendEmailTest([FromQuery] Guid employeeId)
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();

            List<string> approverNames = new List<string>();
            List<string> approverEmails = new List<string>();
            var employeeName = await _userService.GetNameAsync(employeeId, cancellationToken);
            var approverIdList = await _taskService.GetApproversAsync(employeeId, cancellationToken);

            foreach (var approver in approverIdList)
            {
                approverNames.Add(await _userService.GetNameAsync(approver, cancellationToken));
                approverEmails.Add(await _userService.GetEmailAsync(approver, cancellationToken));
            }

            var messages = await _taskService.EmailApproverAsync(approverNames, approverEmails, employeeName, employeeId, cancellationToken);

            foreach (var message in messages)
            {
                _emailSender.SendEmail(message);
            }

            return NoContent();
        }
    }
}