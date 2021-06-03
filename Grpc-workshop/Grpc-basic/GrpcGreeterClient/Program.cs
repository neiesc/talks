using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterServer;

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
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "Edinei" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Id: " + reply.Id);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
