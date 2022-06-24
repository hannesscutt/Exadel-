namespace ExadelTimeTrackingSystem.Data.Configuration.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITimeOutSettings
    {
       int? TimeOutSeconds { get; set; }
    }
}
