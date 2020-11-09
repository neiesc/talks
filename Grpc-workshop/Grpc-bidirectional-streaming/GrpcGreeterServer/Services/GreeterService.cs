using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcGreeter
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override async Task SaveAll(
            IAsyncStreamReader<HelloRequest> requestStream,
            IServerStreamWriter<HelloReply> responseStream,
            ServerCallContext context
        )
        {
            while (await requestStream.MoveNext())
            {
                var helloRequest = requestStream.Current;
                await responseStream.WriteAsync(
                    new HelloReply{ 
                        Id = Guid.NewGuid().ToString(),
                        Message = "Hello " + helloRequest.Name }
                );
            }
        }
    }
}
