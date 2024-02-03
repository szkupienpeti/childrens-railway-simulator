using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class IndulasiIdoKozlesVetelEventArgs : KozlemenyEventArgs
{
    public TimeOnly Ido { get; }

    public IndulasiIdoKozlesVetelEventArgs(AllomasNev kuldo, string vonatszam, TimeOnly ido, string nev)
        : base(kuldo, vonatszam, nev)
    {
        Ido = ido;
    }

    public static IndulasiIdoKozlesVetelEventArgs FromGrpcEventArgs(GrpcRequestEventArgs<IndulasiIdoKozlesVetelRequest> grpcEventArgs)
    {
        IndulasiIdoKozlesVetelRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        TimeOnly ido = GrpcToModelMapper.MapIdo(request.Ido);
        return new(kuldo, request.Vonatszam, ido, request.Nev);
    }
}
