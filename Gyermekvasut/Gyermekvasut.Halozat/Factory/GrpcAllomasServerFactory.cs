using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gyermekvasut.Halozat.Factory;

internal class GrpcAllomasServerFactory : GrpcAllomasFactoryBase
{
    public GrpcAllomasServerFactory(IConfiguration configuration) : base(configuration) { }

    public GrpcAllomasServer CreateAndStart(AllomasNev allomasNev)
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddGrpc();
        builder.Services.AddSingleton(new GrpcAllomasServer());

        var app = builder.Build();
        app.MapGrpcService<GrpcAllomasServer>();
        app.MapGet("/", () => "Gyermekvasut.Grpc.Server.GrpcAllomasServer is working.");

        string address = GetAllomasAddress(allomasNev);
        app.RunAsync(address);

        return (app.Services.GetRequiredService(typeof(GrpcAllomasServer)) as GrpcAllomasServer)!;
    }
}