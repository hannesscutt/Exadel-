namespace ExadelTimeTrackingSystem.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EmailService;
    using ExadelTimeTrackingSystem.BusinessLogic.Helpers;
    using ExadelTimeTrackingSystem.BusinessLogic.Services.Abstract;
    using ExadelTimeTrackingSystem.WebAPI.Configuration;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [ApiController]
    [Route("[controller]")]
    public class HangFireController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IOptionsMonitor<TimeOutSettings> _options;
        private readonly IEmailSender _emailSender;
        private readonly IUserService _userService;

        public HangFireController(IOptionsMonitor<TimeOutSettings> options, IEmailSender emailSender, IUserService userService)
        {
            _options = options;
            _emailSender = emailSender;
            _userService = userService;
        }

        [HttpPost]

        public async Task<ActionResult> WeeklyApproverEmailAsync()
        {
            var cancellationToken = CancellationTokenCreator.Create(_options.CurrentValue.TimeOutSeconds);
            cancellationToken.ThrowIfCancellationRequested();
            return null;
        }
    }
}
