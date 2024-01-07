using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Grpc.Client;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas
{
    private void AllomasServer_GrpcEngedelyKeresEvent(object? sender, GrpcEngedelyKeresEventArgs grpcEventArgs)
    {
        EngedelyKeresEventArgs e = EngedelyKeresEventArgs.FromGrpcEventArgs(grpcEventArgs);
        EngedelyKeresEvent?.Invoke(this, e);
    }

    public void EngedelytKer(Irany irany, EngedelyKeresTipus tipus, string utolsoVonat, string vonatszam, TimeOnly ido, string nev)
    {
        EngedelyKeresRequest request = GrpcRequestFactory.CreateEngedelyKeresRequest(AllomasNev, tipus, utolsoVonat, vonatszam, ido, nev);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.EngedelyKeres(request);
    }
}