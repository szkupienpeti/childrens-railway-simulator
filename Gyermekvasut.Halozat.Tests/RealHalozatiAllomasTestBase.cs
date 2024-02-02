using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class RealHalozatiAllomasTestBase : HalozatiAllomasTestBase<GrpcAllomasServer>
{
    private static readonly string HALOZAT_CONFIG_FILE = "gyermekvasut.halozat.settings.json";

    private GrpcAllomasClient? _szomszedClient;
    protected GrpcAllomasClient SzomszedClient => _szomszedClient!;

    protected void AllomasEsSzomszedClientFelepit(AllomasNev allomasNev)
    {
        var config = BuildTestConfig(HALOZAT_CONFIG_FILE);
        var grpcServerMock = new Mock<GrpcAllomasServer>() { CallBase = true };
        var serverFactory = new GrpcAllomasServerFactory(config);
        serverFactory.Start(grpcServerMock.Object, allomasNev);
        BuildAllomasFromServerMock(config, allomasNev, grpcServerMock, true);
        // Non-mock szomszéd client, ami a tesztelt állomást hívja
        var realClientFactory = new GrpcAllomasClientFactory(config);
        _szomszedClient = realClientFactory.CreateOptional(allomasNev)!;
    }
}
