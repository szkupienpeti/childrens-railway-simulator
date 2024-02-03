using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.Factory;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas
{
    private void AllomasServer_GrpcIndulasiIdoKozlesVetelEvent(object? sender, GrpcRequestEventArgs<IndulasiIdoKozlesVetelRequest> grpcEventArgs)
    {
        IndulasiIdoKozlesVetelEventArgs e = IndulasiIdoKozlesVetelEventArgs.FromGrpcEventArgs(grpcEventArgs);
        IndulasiIdoKozlesVetelEvent?.Invoke(this, e);
    }

    public void IndulasiIdoKozlestVesz(Irany irany, string vonatszam, TimeOnly ido, string nev)
    {
        IndulasiIdoKozlesVetelRequest request = GrpcRequestFactory.CreateIndulasiIdoKozlesVetelRequest(AllomasNev, vonatszam, ido, nev);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.IndulasiIdoKozlesVetel(request);
    }
}