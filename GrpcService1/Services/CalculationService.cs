using Grpc.Core;
using Grpc.Net.Client;
using GrpcService1;
using GrpcService2;

namespace GrpcService1.Services
{
    public class CalculationService : Calculation.CalculationBase
    {
        public override Task<CalcResponse> Sum(CalcRequest request, ServerCallContext context)
        {
            var result = request.Number1 + request.Number2;
            return Task.FromResult(new CalcResponse { Result = result });
        }

        public override async Task<CalcResponse> Subtract(CalcRequest request, ServerCallContext context)
        {
            var channel = GrpcChannel.ForAddress("http://localhost:5225");
            var subtractClient = new Subtract.SubtractClient(channel);
            var subtractResponse = await subtractClient.SubtractAsync(new SubtractRequest
            {
                Number1 = request.Number1,
                Number2 = request.Number2,
            });
            await channel.ShutdownAsync();

            return new CalcResponse { Result = subtractResponse.Result };
        }
    }
}