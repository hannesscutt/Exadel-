namespace ExadelTimeTrackingSystem.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.BusinessLogic.DTOs;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
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
           var projects = await _service.GetAllAsync();
           return Ok(projects);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProjectDTO>> GetByIdAsync([FromRoute] Guid id)
        {
            var project = await _service.GetByIdAsync(id);
            return project == null ? NotFound() : Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> CreateAsync([FromBody] CreateProjectDTO project)
        {
            var projectDto = await _service.CreateAsync(project);
            return Created(string.Empty, projectDto);
        }

        [HttpGet("names")]
        public async Task<ActionResult<List<string>>> GetNamesAsync()
        {
            var names = await _service.GetNamesAsync();
            return Ok(names);
        }

        [HttpGet("{id:guid}/activities")]

        public async Task<ActionResult<List<string>>> GetActivitiesAsync([FromRoute] Guid id)
        {
            var activities = await _service.GetActivitiesAsync(id);
            return activities == null ? NotFound() : Ok(activities);
        }
    }
}
