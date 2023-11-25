using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcVonatAllomaskozbeBelepEvent(object? sender, GrpcVonatAllomaskozbeBelepEventArgs grpcEventArgs)
    {
        VonatAllomaskozbeBelepEventArgs e = VonatAllomaskozbeBelepEventArgs.FromGrpcEventArgs(grpcEventArgs);
        VonatAllomaskozbeBelepEvent?.Invoke(this, e);
    }

    public void VonatotAllomaskozbeBeleptet(Irany irany, Vonat vonat)
    {
        VonatAllomaskozbeBelepRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonat = ModelToGrpcMapper.MapVonat(vonat)
        };
        GetSzomszedClient(irany).VonatAllomaskozbeBelepAsync(request);
    }
}