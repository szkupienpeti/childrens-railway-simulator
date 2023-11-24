using Grpc.Net.Client;

namespace Gyermekvasut.Grpc.Client;

public sealed class GrpcAllomasClient : GrpcAllomas.GrpcAllomasClient
{
    public GrpcAllomasClient(string address) : base(GrpcChannel.ForAddress(address)) { }
}
