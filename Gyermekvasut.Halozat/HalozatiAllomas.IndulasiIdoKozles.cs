using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.Factory;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas
{
    private void AllomasServer_GrpcIndulasiIdoKozlesEvent(object? sender, GrpcIndulasiIdoKozlesEventArgs grpcEventArgs)
    {
        IndulasiIdoKozlesEventArgs e = IndulasiIdoKozlesEventArgs.FromGrpcEventArgs(grpcEventArgs);
        IndulasiIdoKozlesEvent?.Invoke(this, e);
    }

    public void IndulasiIdotKozol(Irany irany, string vonatszam, TimeOnly ido, string nev)
    {
        IndulasiIdoKozlesRequest request = GrpcRequestFactory.CreateIndulasiIdoKozlesRequest(AllomasNev, vonatszam, ido, nev);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.IndulasiIdoKozles(request);
    }
}