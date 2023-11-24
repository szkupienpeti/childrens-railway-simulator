using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcEngedelyKeresEvent(object? sender, GrpcEngedelyKeresEventArgs grpcEventArgs)
    {
        EngedelyKeresEventArgs e = EngedelyKeresEventArgs.FromGrpcEventArgs(grpcEventArgs);
        EngedelyKeresEvent?.Invoke(this, e);
    }

    public void EngedelytKer(HivasIrany irany, EngedelyKeresTipus tipus,
        string utolsoVonat, string vonatszam, TimeOnly ido, string nev)
    {
        EngedelyKeresRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Tipus = ModelToGrpcMapper.MapEngedelyKeresTipus(tipus),
            UtolsoVonat = utolsoVonat,
            Vonatszam = vonatszam,
            Ido = ModelToGrpcMapper.MapTimeOnly(ido),
            Nev = nev
        };
        GetSzomszedClient(irany).EngedelyKeresAsync(request);
    }
}