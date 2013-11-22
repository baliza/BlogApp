using System;
using System.Net.Http;

namespace WebAPI.Tests.Server
{
    public interface IApiServer
    {
        Uri BaseAddress { get; }
        ApiServerHost Kind { get; }
        HttpMessageHandler ServerHandler { get; }
        void Start();
        void Stop();
    }
}
