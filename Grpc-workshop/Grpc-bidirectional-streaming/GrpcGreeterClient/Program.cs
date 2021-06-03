using System;
using System.Runtime.InteropServices;
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
            var serverAddress = "https://localhost:5001";

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                // The following statement allows you to call insecure services. To be used only in development environments.
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                serverAddress = "http://localhost:5000";
            }

            var channel = GrpcChannel.ForAddress(serverAddress);
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
