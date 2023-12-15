using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcVisszajelentesVetelEvent(object? sender, GrpcVisszajelentesVetelEventArgs grpcEventArgs)
    {
        VisszajelentesVetelEventArgs e = VisszajelentesVetelEventArgs.FromGrpcEventArgs(grpcEventArgs);
        VisszajelentesVetelEvent?.Invoke(this, e);
    }

    public void VisszajelentestVesz(Irany irany, string vonatszam, string nev)
    {
        VisszajelentesVetelRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonatszam = vonatszam,
            Nev = nev
        };
        GetSzomszedClient(irany).VisszajelentesVetelAsync(request);
    }
}