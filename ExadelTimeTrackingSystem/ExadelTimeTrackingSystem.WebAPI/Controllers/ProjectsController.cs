namespace ExadelTimeTrackingSystem.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.Data.Validators;
    using ExadelTimeTrackingSystem.WebAPI.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectDTO>>> GetAllAsync()
        {
            var token = ConfigureCancellationToken.Configure();
            var projects = await _service.GetAllAsync(token);
            return Ok(projects);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            var token = ConfigureCancellationToken.Configure();
            var project = await _service.GetByIdAsync(id, token);
            return project == null ? NotFound() : Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateAsync([FromBody] CreateProjectDTO project)
        {
            var token = ConfigureCancellationToken.Configure();
            var projectDto = await _service.CreateAsync(project, token);
            return Created(string.Empty, projectDto);
        }

        [HttpGet("names")]
        public async Task<ActionResult<List<string>>> GetNamesAsync()
        {
            var token = ConfigureCancellationToken.Configure();
            var names = await _service.GetNamesAsync(token);
            return Ok(names);
        }

        [HttpGet("{id:guid}/activities")]

        public async Task<ActionResult<List<string>>> GetActivitiesAsync([FromRoute] Guid id)
        {
            var token = ConfigureCancellationToken.Configure();
            var activities = await _service.GetActivitiesAsync(id, token);
            return activities == null ? NotFound() : Ok(activities);
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDTO>> UpdateProjectAsync([FromBody] ProjectDTO project)
        {
            var token = ConfigureCancellationToken.Configure();
            var projectDto = await _service.UpdateAsync(project, token);
            return Ok(projectDto);
        }
    }
}
