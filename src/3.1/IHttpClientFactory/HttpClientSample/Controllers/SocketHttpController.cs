using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientSample.Controllers
{
    [Route("SocketHttp/[action]")]
    [ApiController]
    public class SocketHttpController : ControllerBase
    {
        /// <summary>
        /// 5 分鐘內，會共用一條連線。
        /// </summary>
        public async Task<IActionResult> Demo1()
        {
            // Ref: https://docs.microsoft.com/en-us/dotnet/api/system.net.http.socketshttphandler?view=netcore-3.1
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(10),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(5),
                MaxConnectionsPerServer = 10
            };

            var client = new HttpClient(socketsHandler);
            HttpResponseMessage response = null;
            for (var i = 0; i < 5; i++)
            {
                response = await client.GetAsync("https://www.google.com");
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync();
            return Ok(result);
        }

        /// <summary>
        /// 共有 5 條連線。因為每一條連線只存活一秒。
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Demo2()
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromSeconds(1),
                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(1),
                MaxConnectionsPerServer = 10
            };

            var client = new HttpClient(socketsHandler);
            HttpResponseMessage response = null;
            for (var i = 0; i < 5; i++)
            {
                response = await client.GetAsync("https://www.google.com");
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync();
            return Ok(result);
        }

        /// <summary>
        /// 60 秒內，最多 2 條連線。
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Demo3()
        {
            var socketsHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromSeconds(60),
                PooledConnectionIdleTimeout = TimeSpan.FromSeconds(60),
                MaxConnectionsPerServer = 2
            };

            var client = new HttpClient(socketsHandler);
            HttpResponseMessage response = null;
            for (var i = 0; i < 5; i++)
            {
                response = await client.GetAsync("https://www.google.com");
                await Task.Delay(TimeSpan.FromSeconds(2));
            }

            response.EnsureSuccessStatusCode();

            var result = response.Content.ReadAsStringAsync();
            return Ok(result);
        }
    }
}