namespace ExadelTimeTrackingSystem.BusinessLogic.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CancellationTokenCreator
    {
        public static CancellationToken Create(int cancellationTokenTimeOut)
        {
            if (cancellationTokenTimeOut.Equals(null))
            {
                return new CancellationTokenSource().Token;
            }
            else
            {
                return new CancellationTokenSource(TimeSpan.FromSeconds(cancellationTokenTimeOut)).Token;
            }
        }
    }
}
