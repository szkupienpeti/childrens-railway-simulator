using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

public class MockGrpcAllomasClientFactory : GrpcAllomasFactoryBase
{
    public MockGrpcAllomasClientFactory(IConfiguration configuration) : base(configuration) { }

    public Mock<GrpcAllomasClient>? CreateOptional(AllomasNev? allomasNevOpt, bool callBase)
    {
        if (allomasNevOpt == null)
        {
            return null;
        }
        AllomasNev allomasNev = (AllomasNev)allomasNevOpt;
        string address = GetAllomasAddress(allomasNev);
        return new Mock<GrpcAllomasClient>(address) { CallBase = callBase };
    }
}
