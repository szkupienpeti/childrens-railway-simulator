using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gyermekvasut.Halozat.Factory;

public class GrpcAllomasServerFactory : GrpcAllomasFactoryBase
{
    public GrpcAllomasServerFactory(IConfiguration configuration) : base(configuration) { }

    public static GrpcAllomasServer CreateOnly()
        => new();

    public void Start(GrpcAllomasServer grpcAllomasServer, AllomasNev allomasNev)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddGrpc();
        builder.Services.AddSingleton(grpcAllomasServer);

        var app = builder.Build();
        app.MapGrpcService<GrpcAllomasServer>();
        app.MapGet("/", () => "Gyermekvasut.Grpc.Server.GrpcAllomasServer is working.");
        grpcAllomasServer.SetApp(app);
        
        string address = GetAllomasAddress(allomasNev);
        app.RunAsync(address);
    }

    public GrpcAllomasServer CreateAndStart(AllomasNev allomasNev)
    {
        GrpcAllomasServer grpcAllomasServer = CreateOnly();
        Start(grpcAllomasServer, allomasNev);
        return grpcAllomasServer;
    }
}