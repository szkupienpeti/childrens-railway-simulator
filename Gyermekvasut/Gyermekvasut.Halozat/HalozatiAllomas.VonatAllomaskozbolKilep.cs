using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcVonatAllomaskozbolKilepEvent(object? sender, GrpcVonatAllomaskozbolKilepEventArgs grpcEventArgs)
    {
        VonatAllomaskozbolKilepEventArgs e = VonatAllomaskozbolKilepEventArgs.FromGrpcEventArgs(grpcEventArgs);
        VonatAllomaskozbolKilepEvent?.Invoke(this, e);
    }

    public void VonatotAllomaskozolKileptet(Irany irany, string vonatszam)
    {
        VonatAllomaskozbolKilepRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonatszam = vonatszam
        };
        GetSzomszedClient(irany).VonatAllomaskozbolKilepAsync(request);
    }
}