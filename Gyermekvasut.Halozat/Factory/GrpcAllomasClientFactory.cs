using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;

namespace Gyermekvasut.Halozat.Factory;

public class GrpcAllomasClientFactory : GrpcAllomasFactoryBase
{
    public GrpcAllomasClientFactory(IConfiguration configuration) : base(configuration) { }

    public GrpcAllomasClient? CreateOptional(AllomasNev? allomasNevOpt)
    {
        if (allomasNevOpt == null)
        {
            return null;
        }
        AllomasNev allomasNev = (AllomasNev)allomasNevOpt;
        string address = GetAllomasAddress(allomasNev);
        return new GrpcAllomasClient(address);
    }
}
