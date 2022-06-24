namespace ExadelTimeTrackingSystem.WebAPI.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ExadelTimeTrackingSystem.Data.Configuration.Abstract;

    public class TimeOutSettings : ITimeOutSettings
    {
        public int? TimeOutSeconds { get; set; }
    }
}
