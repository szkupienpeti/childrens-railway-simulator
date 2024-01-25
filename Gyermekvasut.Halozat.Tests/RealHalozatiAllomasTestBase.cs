using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class RealHalozatiAllomasTestBase : HalozatiAllomasTestBase
{
    private static readonly string HALOZAT_CONFIG_FILE = "gyermekvasut.halozat.settings.json";

    // TODO törölni
    private HalozatiAllomasFactory? _allomasFactory;
    protected HalozatiAllomasFactory AllomasFactory => _allomasFactory!;

    private GrpcAllomasClient? _szomszedClient;
    protected GrpcAllomasClient SzomszedClient => _szomszedClient!;

    // TODO törölni
    protected void AllomasFelepit(AllomasNev allomasNev)
    {
        _allomasFactory = new(BuildTestConfig());
        _allomas = AllomasFactory.Create(allomasNev);
    }

    protected void AllomasEsSzomszedClientFelepit(AllomasNev allomasNev)
    {
        var testConfig = BuildTestConfig();
        var allomasFactory = new HalozatiAllomasFactory(testConfig);
        _allomas = allomasFactory.Create(allomasNev);
        var clientFactory = new GrpcAllomasClientFactory(testConfig);
        _szomszedClient = clientFactory.CreateOptional(allomasNev)!;
    }

    private static IConfiguration BuildTestConfig()
    {
        var configBuilder = new HalozatConfigurationBuilder();
        configBuilder.AddJsonFile(HALOZAT_CONFIG_FILE);
        return configBuilder.Build();
    }
}
