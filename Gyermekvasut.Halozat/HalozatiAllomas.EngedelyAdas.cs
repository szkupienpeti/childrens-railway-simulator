using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Grpc.Client;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas
{
    private void AllomasServer_GrpcEngedelyAdasEvent(object? sender, GrpcEngedelyAdasEventArgs grpcEventArgs)
    {
        EngedelyAdasEventArgs e = EngedelyAdasEventArgs.FromGrpcEventArgs(grpcEventArgs);
        EngedelyAdasEvent?.Invoke(this, e);
    }

    public void EngedelytAd(Irany irany, EngedelyAdasTipus tipus, string utolsoVonat, string vonatszam, string nev)
    {
        EngedelyAdasRequest request = GrpcRequestFactory.CreateEngedelyAdasRequest(AllomasNev, tipus, utolsoVonat, vonatszam, nev);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.EngedelyAdas(request);
    }
}