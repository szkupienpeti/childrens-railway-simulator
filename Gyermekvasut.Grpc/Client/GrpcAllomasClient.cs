using Grpc.Net.Client;

namespace Gyermekvasut.Grpc.Client;

public class GrpcAllomasClient : GrpcAllomas.GrpcAllomasClient
{
    public GrpcAllomasClient(string address) : base(GrpcChannel.ForAddress(address)) { }
}
