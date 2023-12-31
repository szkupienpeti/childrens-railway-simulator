using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Grpc.Client;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcCsengetesEvent(object? sender, GrpcCsengetesEventArgs grpcEventArgs)
    {
        CsengetesEventArgs e = CsengetesEventArgs.FromGrpcEventArgs(grpcEventArgs);
        CsengetesEvent?.Invoke(this, e);
    }

    public void Csenget(Irany irany, List<Csengetes> csengetesek)
    {
        CsengetesRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev)
        };
        ModelToGrpcMapper.FillRepeated(request.Csengetesek, csengetesek, ModelToGrpcMapper.MapCsengetes);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.Csengetes(request);
    }
}