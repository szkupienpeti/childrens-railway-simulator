using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.Extensions.Configuration;

namespace Gyermekvasut.Halozat;

public class HalozatiAllomasFactory
{
    private IConfiguration Configuration { get; }
    private GrpcAllomasServerFactory ServerFactory { get; }
    private GrpcAllomasClientFactory ClientFactory { get; }

    public HalozatiAllomasFactory(IConfiguration configuration)
    {
        Configuration = configuration;
        ServerFactory = new(Configuration);
        ClientFactory = new(Configuration);
    }

    public HalozatiAllomas Create(AllomasNev allomasNev)
    {
        IGrpcAllomasServer allomasServer = ServerFactory.CreateAndStart(allomasNev);
        GrpcAllomasClient? kpAllomasClient = ClientFactory.CreateOptional(allomasNev.KpSzomszed());
        GrpcAllomasClient? vpAllomasClient = ClientFactory.CreateOptional(allomasNev.VpSzomszed());
        return new(allomasNev, allomasServer, kpAllomasClient, vpAllomasClient);
    }
}
