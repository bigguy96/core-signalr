using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs
{
    public class StreamHub : Hub
    {
        public async IAsyncEnumerable<byte[]> SendDataRow()
        {
            var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            var files = System.IO.Directory.GetFiles( myDocuments,"*.jpg");

            foreach (var file in files)
            {
                var bytes = await File.ReadAllBytesAsync(file);
                
                await using var ms = new MemoryStream(bytes);
                
                yield return ms.ToArray();
            }
        }
    }
}

//https://github.com/codehaks/StreamServerDemo/blob/master/MyServer/Hubs/StreamHub.cs