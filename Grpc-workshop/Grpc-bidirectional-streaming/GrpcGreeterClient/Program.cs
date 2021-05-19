using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeter;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            Console.WriteLine("Enter para continuar: ");
            Console.ReadKey();

            using (var call = client.SaveAll())
            {
                var responseReaderTask = Task.Run(async () =>
                {
                    while (await call.ResponseStream.MoveNext(CancellationToken.None))
                    {
                        var helloReply = call.ResponseStream.Current;
                        Console.WriteLine("Received " + helloReply);
                    }
                });

                for (int i = 0; i < 10; i++)
                {
                    var helloRequest = new HelloRequest { Name = "Edinei" };
                    await call.RequestStream.WriteAsync(helloRequest);
                    Console.WriteLine("Send " + helloRequest);
                }
                await call.RequestStream.CompleteAsync();
                await responseReaderTask;
            }
        }
    }
}
