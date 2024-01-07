using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.Factory;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas
{
    private void AllomasServer_GrpcVisszajelentesEvent(object? sender, GrpcVisszajelentesEventArgs grpcEventArgs)
    {
        VisszajelentesEventArgs e = VisszajelentesEventArgs.FromGrpcEventArgs(grpcEventArgs);
        VisszajelentesEvent?.Invoke(this, e);
    }

    public void Visszajelent(Irany irany, string vonatszam, string nev)
    {
        VisszajelentesRequest request = GrpcRequestFactory.CreateVisszajelentesRequest(AllomasNev, vonatszam, nev);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.Visszajelentes(request);
    }
}