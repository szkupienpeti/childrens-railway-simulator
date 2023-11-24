using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcVonatAllomaskozbolKilepEvent(object? sender, GrpcVonatAllomaskozbolKilepEventArgs grpcEventArgs)
    {
        VonatAllomaskozbolKilepEventArgs e = VonatAllomaskozbolKilepEventArgs.FromGrpcEventArgs(grpcEventArgs);
        VonatAllomaskozbolKilepEvent?.Invoke(this, e);
    }

    public void VonatotAllomaskozolKileptet(HivasIrany irany, string vonatszam)
    {
        VonatAllomaskozbolKilepRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonatszam = vonatszam
        };
        GetSzomszedClient(irany).VonatAllomaskozbolKilepAsync(request);
    }
}