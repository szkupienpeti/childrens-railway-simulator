using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Halozat.EventArgsNS;

public class VonatAllomaskozbolKilepEventArgs : HalozatiAllomasEventArgs
{
    public string Vonatszam { get; }
    public VonatAllomaskozbolKilepEventArgs(AllomasNev kuldo, string vonatszam) : base(kuldo)
    {
        Vonatszam = vonatszam;
    }

    public static VonatAllomaskozbolKilepEventArgs FromGrpcEventArgs(GrpcVonatAllomaskozbolKilepEventArgs grpcEventArgs)
    {
        VonatAllomaskozbolKilepRequest request = grpcEventArgs.Request;
        AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
        return new(kuldo, request.Vonatszam);
    }
}
