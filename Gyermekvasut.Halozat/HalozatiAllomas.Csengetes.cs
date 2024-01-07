using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.Factory;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas
{
    private void AllomasServer_GrpcCsengetesEvent(object? sender, GrpcCsengetesEventArgs grpcEventArgs)
    {
        CsengetesEventArgs e = CsengetesEventArgs.FromGrpcEventArgs(grpcEventArgs);
        CsengetesEvent?.Invoke(this, e);
    }

    public void Csenget(Irany irany, List<Csengetes> csengetesek)
    {
        CsengetesRequest request = GrpcRequestFactory.CreateCsengetesRequest(AllomasNev, csengetesek);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.Csengetes(request);
    }
}