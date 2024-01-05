using Grpc.Core;
using GrpcService2;

namespace GrpcService2.Services
{
    public class SubtractService : Subtract.SubtractBase
    {
        public override Task<SubtractResponse> Subtract(SubtractRequest request, ServerCallContext context)
        {
            var result = request.Number1 - request.Number2;
            return Task.FromResult(new SubtractResponse { Result = result});
            
        }
    }
}