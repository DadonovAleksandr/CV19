using System;

namespace CV19.Services;

interface IAsyncDataService
{
    string GetResult(DateTime time);
}