using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeterServer;

namespace GrpcGreeterClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
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
