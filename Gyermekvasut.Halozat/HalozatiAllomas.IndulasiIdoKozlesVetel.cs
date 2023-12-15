using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcIndulasiIdoKozlesVetelEvent(object? sender, GrpcIndulasiIdoKozlesVetelEventArgs grpcEventArgs)
    {
        IndulasiIdoKozlesVetelEventArgs e = IndulasiIdoKozlesVetelEventArgs.FromGrpcEventArgs(grpcEventArgs);
        IndulasiIdoKozlesVetelEvent?.Invoke(this, e);
    }

    public void IndulasiIdoKozlestVesz(Irany irany, string vonatszam, TimeOnly ido, string nev)
    {
        IndulasiIdoKozlesVetelRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonatszam = vonatszam,
            Ido = ModelToGrpcMapper.MapTimeOnly(ido),
            Nev = nev
        };
        GetSzomszedClient(irany).IndulasiIdoKozlesVetelAsync(request);
    }
}