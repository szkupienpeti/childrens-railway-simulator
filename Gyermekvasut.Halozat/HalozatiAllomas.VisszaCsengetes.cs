using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{        
    private void AllomasServer_GrpcVisszaCsengetesEvent(object? sender, GrpcVisszaCsengetesEventArgs grpcEventArgs)
    {
        VisszaCsengetesEventArgs e = VisszaCsengetesEventArgs.FromGrpcEventArgs(grpcEventArgs);
        VisszaCsengetesEvent?.Invoke(this, e);
    }

    public void VisszaCsenget(Irany irany, List<Csengetes> csengetesek)
    {
        VisszaCsengetesRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev)
        };
        ModelToGrpcMapper.FillRepeated(request.Csengetesek, csengetesek, ModelToGrpcMapper.MapCsengetes);
        GetSzomszedClient(irany).VisszaCsengetes(request);
    }
}