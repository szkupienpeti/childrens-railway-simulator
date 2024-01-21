using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;

namespace Gyermekvasut.Halozat;

public class HalozatiAllomasFactory
{
    private IConfiguration Configuration { get; }

    public HalozatiAllomasFactory(IConfiguration configuration)
    {
        Configuration = configuration;
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
