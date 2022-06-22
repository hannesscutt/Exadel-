namespace ExadelTimeTrackingSystem.WebAPI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ConfigureCancellationToken
    {
        public static CancellationToken Configure()
        {
            CancellationTokenSource source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
            return source.Token;
        }
    }
}
