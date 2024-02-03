using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class EngedelyMegtagadasEventArgs : KozlemenyEventArgs
{
    public string Ok { get; }
    public int PercMulva { get; }

    public EngedelyMegtagadasEventArgs(AllomasNev kuldo, string vonatszam, string ok, int percMulva, string nev)
        : base(kuldo, vonatszam, nev)
    {
        Ok = ok;
        PercMulva = percMulva;
    }

    public static EngedelyMegtagadasEventArgs FromGrpcEventArgs(GrpcRequestEventArgs<EngedelyMegtagadasRequest> grpcEventArgs)
    {
        EngedelyMegtagadasRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        return new(kuldo, request.Vonatszam, request.Ok, request.PercMulva, request.Nev);
    }
}
