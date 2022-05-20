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

        [HttpGet]
        public async Task<ActionResult<ProjectDTO>> GetByIdAsync(Guid id)
        {
            var project = await _service.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(project);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProjectDTO>> CreateAsync(ProjectDTO project)
        {
            await _service.CreateAsync(project);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = project.Id }, project);
        }
    }
}
