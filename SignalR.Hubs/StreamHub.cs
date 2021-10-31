using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class StreamHub : Hub
    {
        public async IAsyncEnumerable<string> SendDataRow()
        {
            for (var i = 0; i < 100; i++)
            {
                await Task.Delay(100);
                yield return i.ToString();
            }
        }
    }
}

//https://github.com/codehaks/StreamServerDemo/blob/master/MyServer/Hubs/StreamHub.cs