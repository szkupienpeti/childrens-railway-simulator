using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class VisszajelentesVetelEventArgs : KozlemenyEventArgs
{
    public VisszajelentesVetelEventArgs(AllomasNev kuldo, string vonatszam, string nev)
        : base(kuldo, vonatszam, nev) { }

    public static VisszajelentesVetelEventArgs FromGrpcEventArgs(GrpcVisszajelentesVetelEventArgs grpcEventArgs)
    {
        VisszajelentesVetelRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        return new(kuldo, request.Vonatszam, request.Nev);
    }
}
