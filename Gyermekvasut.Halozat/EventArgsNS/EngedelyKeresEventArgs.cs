using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class EngedelyKeresEventArgs : KozlemenyEventArgs
{
    public EngedelyKeresTipus Tipus { get; }
    public string UtolsoVonat { get;}
    public TimeOnly Ido { get; }

    public EngedelyKeresEventArgs(AllomasNev kuldo, EngedelyKeresTipus tipus,
        string utolsoVonat, string vonatszam, TimeOnly ido, string nev) : base(kuldo, vonatszam, nev)
    {
        UtolsoVonat = utolsoVonat;
        Tipus = tipus;
        Ido = ido;
    }

    public static EngedelyKeresEventArgs FromGrpcEventArgs(GrpcRequestEventArgs<EngedelyKeresRequest> grpcEventArgs)
    {
        EngedelyKeresRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        EngedelyKeresTipus tipus = GrpcToModelMapper.MapEngedelyKeresTipus(request.Tipus);
        TimeOnly ido = GrpcToModelMapper.MapIdo(request.Ido);
        return new(kuldo, tipus, request.UtolsoVonat, request.Vonatszam, ido, request.Nev);
    }
}
