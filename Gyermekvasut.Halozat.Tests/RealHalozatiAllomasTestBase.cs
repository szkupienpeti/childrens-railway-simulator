using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class RealHalozatiAllomasTestBase : HalozatiAllomasTestBase
{
    private static readonly string HALOZAT_CONFIG_FILE = "gyermekvasut.halozat.settings.json";

    private GrpcAllomasClient? _szomszedClient;
    protected GrpcAllomasClient SzomszedClient => _szomszedClient!;

    private Mock<GrpcAllomasServer>? _grpcServerMock;
    protected Mock<GrpcAllomasServer> GrpcServerMock => _grpcServerMock!;

    protected void AllomasEsSzomszedClientFelepit(AllomasNev allomasNev)
    {
        var testConfig = BuildTestConfig();
        _grpcServerMock = new Mock<GrpcAllomasServer>() { CallBase = true };
        var serverFactory = new GrpcAllomasServerFactory(testConfig);
        serverFactory.Start(GrpcServerMock.Object, allomasNev);
        var clientFactory = new GrpcAllomasClientFactory(testConfig);
        var kpClient = clientFactory.CreateOptional(allomasNev.KpSzomszed());
        var vpClient = clientFactory.CreateOptional(allomasNev.VpSzomszed());
        _allomas = new(allomasNev, GrpcServerMock.Object, kpClient, vpClient);
        _szomszedClient = clientFactory.CreateOptional(allomasNev)!;
    }

    private static IConfiguration BuildTestConfig()
    {
        var configBuilder = new HalozatConfigurationBuilder();
        configBuilder.AddJsonFile(HALOZAT_CONFIG_FILE);
        return configBuilder.Build();
    }
}
