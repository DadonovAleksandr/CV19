using System;
using System.Threading;

namespace CV19.Services.Interfaces;

class AsyncDataService : IAsyncDataService
{
    public string GetResult(DateTime time)
    {
        Thread.Sleep(10_000);

        return $"Result value {time}";
    }
}