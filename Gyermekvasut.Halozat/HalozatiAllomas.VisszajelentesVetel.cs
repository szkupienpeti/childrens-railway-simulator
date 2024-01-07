using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.Factory;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas
{
    private void AllomasServer_GrpcVisszajelentesVetelEvent(object? sender, GrpcVisszajelentesVetelEventArgs grpcEventArgs)
    {
        VisszajelentesVetelEventArgs e = VisszajelentesVetelEventArgs.FromGrpcEventArgs(grpcEventArgs);
        VisszajelentesVetelEvent?.Invoke(this, e);
    }

    public void VisszajelentestVesz(Irany irany, string vonatszam, string nev)
    {
        VisszajelentesVetelRequest request = GrpcRequestFactory.CreateVisszajelentesVetelRequest(AllomasNev, vonatszam, nev);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.VisszajelentesVetel(request);
    }
}