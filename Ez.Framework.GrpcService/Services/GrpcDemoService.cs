using Grpc.Core;
using GrpcDemoGrpc;

namespace Ez.Framework.GrpcService.Services;

public class GrpcDemoService:GrpcDemo.GrpcDemoBase
{
    public override async Task<CreateGrpcDemoResponse> CreateGrpcDemo(CreateGrpcDemoRequest request,
        ServerCallContext context)
    {
        if(request.Title ==string.Empty||request.Description==string.Empty) 
            throw new RpcException(new Status(StatusCode.InvalidArgument, "grpcError"));
        return await Task.FromResult(new CreateGrpcDemoResponse
        {
            Id = 11111
        });
    }
}   