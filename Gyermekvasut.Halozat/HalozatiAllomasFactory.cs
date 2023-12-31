using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;

namespace Gyermekvasut.Halozat;

public class HalozatiAllomasFactory
{
    private static readonly string HALOZAT_CONFIG_FILE = "gyermekvasut.halozat.settings.json";

    private IConfiguration Configuration { get; }

    public HalozatiAllomasFactory()
    {
        Configuration = new ConfigurationBuilder()
            .AddJsonFile(HALOZAT_CONFIG_FILE)
            .Build();
    }

    public HalozatiAllomas Create(AllomasNev allomasNev)
    {
        GrpcAllomasServerFactory serverFactory = new(Configuration);
        IGrpcAllomasServer allomasServer = serverFactory.CreateAndStart(allomasNev);
        GrpcAllomasClientFactory clientFactory = new(Configuration);
        GrpcAllomasClient? kpAllomasClient = clientFactory.CreateOptional(allomasNev.KpSzomszed());
        GrpcAllomasClient? vpAllomasClient = clientFactory.CreateOptional(allomasNev.VpSzomszed());
        return new(allomasNev, allomasServer, kpAllomasClient, vpAllomasClient);
    }
}
