namespace ExadelTimeTrackingSystem.BusinessLogic.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public static class CancellationTokenCreator
    {
        public static CancellationToken Create(int? cancellationTokenTimeOut)
        {
            if (!cancellationTokenTimeOut.HasValue)
            {
                return CancellationToken.None;
            }
            else
            {
                return new CancellationTokenSource(cancellationTokenTimeOut.Value).Token;
            }
        }
    }
}
