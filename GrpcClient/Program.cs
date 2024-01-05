using Grpc.Net.Client;
using GrpcService1;
using GrpcService2;

namespace GrpcClient 
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var number1 = 100;
            var number2 = 25;

            Console.WriteLine($"Number1 : {number1}");
            Console.WriteLine($"Number2 : {number2}");
            Console.WriteLine("-----------------------");

            var channel = GrpcChannel.ForAddress("http://localhost:5041");
            var calculationClient = new Calculation.CalculationClient(channel);
            await Sum(calculationClient, number1, number2);
            await Subtract(calculationClient, number1, number2);

           await channel.ShutdownAsync();

            Console.ReadLine();
            Console.WriteLine("-----------------------");
            Console.WriteLine("Channel Clossed");
            Console.ReadLine();


        }

        static async Task Sum(Calculation.CalculationClient calculationClient, int number1, int number2)
        {
            var sumResult = await calculationClient.SumAsync(new CalcRequest { Number1 = number1, Number2 = number2 },deadline: DateTime.UtcNow.AddSeconds(5));
            Console.WriteLine($"Sum Result: {sumResult.Result}");
        }

        static async Task Subtract(Calculation.CalculationClient calculationClient, int number1, int number2)
        {
            var sumResult = await calculationClient.SumAsync(new CalcRequest { Number1 = number1, Number2 = number2 });
            Console.WriteLine($"subtract Result: {sumResult.Result}");
        }
    }
}