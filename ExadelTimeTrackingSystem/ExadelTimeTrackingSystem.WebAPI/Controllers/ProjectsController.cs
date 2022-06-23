namespace ExadelTimeTrackingSystem.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Helpers;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Validators;
    using ExadelTimeTrackingSystem.WebAPI.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly int _cancellationTokenTimeOut;

        public ProjectsController(IProjectService service, IOptions<MongoDbSettings> config)
        {
            _service = service;
            _cancellationTokenTimeOut = config.Value.CancellationTokenTimeOut;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetAllAsync()
        {
            var cancellationToken = CancellationTokenCreator.Create(_cancellationTokenTimeOut);
            cancellationToken.ThrowIfCancellationRequested();
            var projects = await _service.GetAllAsync(cancellationToken);
            return Ok(projects);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            var cancellationToken = CancellationTokenCreator.Create(_cancellationTokenTimeOut);
            cancellationToken.ThrowIfCancellationRequested();
            var project = await _service.GetByIdAsync(id, cancellationToken);
            return project == null ? NotFound() : Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateAsync([FromBody] CreateProjectDTO project)
        {
            var cancellationToken = CancellationTokenCreator.Create(_cancellationTokenTimeOut);
            cancellationToken.ThrowIfCancellationRequested();
            var projectDto = await _service.CreateAsync(project, cancellationToken);
            return Created(string.Empty, projectDto);
        }

        [HttpGet("names")]
        public async Task<ActionResult<List<string>>> GetNamesAsync()
        {
            var cancellationToken = CancellationTokenCreator.Create(_cancellationTokenTimeOut);
            cancellationToken.ThrowIfCancellationRequested();
            var names = await _service.GetNamesAsync(cancellationToken);
            return Ok(names);
        }

        [HttpGet("{id:guid}/activities")]

        public async Task<ActionResult<List<string>>> GetActivitiesAsync([FromRoute] Guid id)
        {
            var cancellationToken = CancellationTokenCreator.Create(_cancellationTokenTimeOut);
            cancellationToken.ThrowIfCancellationRequested();
            var activities = await _service.GetActivitiesAsync(id, cancellationToken);
            return activities == null ? NotFound() : Ok(activities);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDTO>> UpdateProjectAsync([FromBody] ProjectDTO project)
        {
            var cancellationToken = CancellationTokenCreator.Create(_cancellationTokenTimeOut);
            cancellationToken.ThrowIfCancellationRequested();
            var projectDto = await _service.UpdateAsync(project, cancellationToken);
            return Ok(projectDto);
        }
    }
}
