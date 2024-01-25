using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class VisszaCsengetesEventArgs : HalozatiAllomasEventArgs
{
    public List<Csengetes> Csengetesek { get; }

    public VisszaCsengetesEventArgs(AllomasNev kuldo, List<Csengetes> csengetesek) : base(kuldo)
    {
        Csengetesek = csengetesek;
    }

    public static VisszaCsengetesEventArgs FromGrpcEventArgs(GrpcRequestEventArgs<VisszaCsengetesRequest> grpcEventArgs)
    {
        VisszaCsengetesRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        List<Csengetes> csengetesek = GrpcToModelMapper.MapRepeated(request.Csengetesek, GrpcToModelMapper.MapCsengetes);
        return new(kuldo, csengetesek);
    }
}
