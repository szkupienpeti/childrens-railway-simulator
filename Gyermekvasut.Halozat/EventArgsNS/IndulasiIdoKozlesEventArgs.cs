using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class IndulasiIdoKozlesEventArgs : KozlemenyEventArgs
{
    public TimeOnly Ido { get; }

    public IndulasiIdoKozlesEventArgs(AllomasNev kuldo, string vonatszam, TimeOnly ido, string nev)
        : base(kuldo, vonatszam, nev)
    {
        Ido = ido;
    }

    public static IndulasiIdoKozlesEventArgs FromGrpcEventArgs(GrpcRequestEventArgs<IndulasiIdoKozlesRequest> grpcEventArgs)
    {
        IndulasiIdoKozlesRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        TimeOnly ido = GrpcToModelMapper.MapIdo(request.Ido);
        return new(kuldo, request.Vonatszam, ido, request.Nev);
    }
}
