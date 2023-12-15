using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Telefon;

namespace Gyermekvasut.Halozat.EventArgsNS
{
    public class EngedelyAdasEventArgs : KozlemenyEventArgs
    {
        public EngedelyAdasTipus Tipus { get; }
        public string UtolsoVonat { get;}

        public EngedelyAdasEventArgs(AllomasNev kuldo, EngedelyAdasTipus tipus,
            string utolsoVonat, string vonatszam, string nev) : base(kuldo, vonatszam, nev)
        {
            UtolsoVonat = utolsoVonat;
            Tipus = tipus;
        }

        public static EngedelyAdasEventArgs FromGrpcEventArgs(GrpcEngedelyAdasEventArgs grpcEventArgs)
        {
            EngedelyAdasRequest request = grpcEventArgs.Request;
            AllomasNev kuldo = GrpcToModelMapper.MapAllomasNev(request.Kuldo);
            EngedelyAdasTipus tipus = GrpcToModelMapper.MapEngedelyAdasTipus(request.Tipus);
            return new(kuldo, tipus, request.UtolsoVonat, request.Vonatszam, request.Nev);
        }
    }
}
