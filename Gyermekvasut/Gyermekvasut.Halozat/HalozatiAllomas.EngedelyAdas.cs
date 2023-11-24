using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcEngedelyAdasEvent(object? sender, GrpcEngedelyAdasEventArgs grpcEventArgs)
    {
        EngedelyAdasEventArgs e = EngedelyAdasEventArgs.FromGrpcEventArgs(grpcEventArgs);
        EngedelyAdasEvent?.Invoke(this, e);
    }

    public void EngedelytAd(HivasIrany irany, EngedelyAdasTipus tipus,
        string utolsoVonat, string vonatszam, string nev)
    {
        EngedelyAdasRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Tipus = ModelToGrpcMapper.MapEngedelyAdasTipus(tipus),
            UtolsoVonat = utolsoVonat,
            Vonatszam = vonatszam,
            Nev = nev
        };
        GetSzomszedClient(irany).EngedelyAdasAsync(request);
    }
}