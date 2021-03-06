using Grpc.Net.Client;
using GrpcServerProject;
using System;
using System.Threading.Tasks;

namespace GrpcClientProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Client Start.");

            var channel = GrpcChannel.ForAddress("https://localhost:5001");

            await sayHello(channel);

            await getCustomerInfo(channel, 1);
            await getCustomerInfo(channel, 2);

            Console.WriteLine("Client End.");

            Console.ReadLine();
        }

        private static async Task sayHello(GrpcChannel channel)
        {
            var input = new HelloRequest { Name = "Khaled" };

            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message);
        }

        private static async Task getCustomerInfo(GrpcChannel channel, int userId)
        {
            var client = new Customer.CustomerClient(channel);

            var model = new CustomerLookupModel { UserId = userId };

            var reply = await client.GetCustomerInfoAsync(model);

            Console.WriteLine(reply.FirstName + " " + reply.LastName );
        }
    }
}
