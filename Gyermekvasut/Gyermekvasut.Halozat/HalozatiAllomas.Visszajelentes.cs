using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcVisszajelentesEvent(object? sender, GrpcVisszajelentesEventArgs grpcEventArgs)
    {
        VisszajelentesEventArgs e = VisszajelentesEventArgs.FromGrpcEventArgs(grpcEventArgs);
        VisszajelentesEvent?.Invoke(this, e);
    }

    public void Visszajelent(HivasIrany irany, string vonatszam, string nev)
    {
        VisszajelentesRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonatszam = vonatszam,
            Nev = nev
        };
        GetSzomszedClient(irany).VisszajelentesAsync(request);
    }
}