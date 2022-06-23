namespace ExadelTimeTrackingSystem.BusinessLogic.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CancellationTokenCreator
    {
        public static CancellationToken Create()
        {
            var source = new CancellationTokenSource(TimeSpan.FromSeconds(0));
            return source.Token;
        }
    }
}
