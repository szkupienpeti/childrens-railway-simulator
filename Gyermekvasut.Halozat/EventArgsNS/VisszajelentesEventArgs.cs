using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class VisszajelentesEventArgs : KozlemenyEventArgs
{
    public VisszajelentesEventArgs(AllomasNev kuldo, string vonatszam, string nev)
        : base(kuldo, vonatszam, nev) { }

    public static VisszajelentesEventArgs FromGrpcEventArgs(GrpcVisszajelentesEventArgs grpcEventArgs)
    {
        VisszajelentesRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        return new(kuldo, request.Vonatszam, request.Nev);
    }
}
