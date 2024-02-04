using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class EngedelyAdasEventArgs : KozlemenyEventArgs
{
    public EngedelyAdasTipus Tipus { get; }
    public string? UtolsoVonat { get;}

    public EngedelyAdasEventArgs(AllomasNev kuldo, EngedelyAdasTipus tipus,
        string? utolsoVonat, string vonatszam, string nev) : base(kuldo, vonatszam, nev)
    {
        UtolsoVonat = utolsoVonat;
        Tipus = tipus;
    }

    public static EngedelyAdasEventArgs FromGrpcEventArgs(GrpcRequestEventArgs<EngedelyAdasRequest> grpcEventArgs)
    {
        EngedelyAdasRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        EngedelyAdasTipus tipus = GrpcToModelMapper.MapEngedelyAdasTipus(request.Tipus);
        if (request.HasUtolsoVonat == (tipus == EngedelyAdasTipus.AzonosIranyu))
        {
            throw new ArgumentException("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélyadás");
        }
        string? utolsoVonat = tipus == EngedelyAdasTipus.AzonosIranyu ? null : request.UtolsoVonat;
        return new(kuldo, tipus, utolsoVonat, request.Vonatszam, request.Nev);
    }
}
