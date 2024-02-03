using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek;
using Gyermekvasut.Halozat.Factory;
using Gyermekvasut.Grpc.Client;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas
{
    private void AllomasServer_GrpcEngedelyMegtagadasEvent(object? sender, GrpcRequestEventArgs<EngedelyMegtagadasRequest> grpcEventArgs)
    {
        EngedelyMegtagadasEventArgs e = EngedelyMegtagadasEventArgs.FromGrpcEventArgs(grpcEventArgs);
        EngedelyMegtagadasEvent?.Invoke(this, e);
    }

    public void EngedelytMegtagad(Irany irany, string vonatszam, string ok, int percMulva, string nev)
    {
        EngedelyMegtagadasRequest request = GrpcRequestFactory.CreateEngedelyMegtagadasRequest(AllomasNev, vonatszam, ok, percMulva, nev);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.EngedelyMegtagadas(request);
    }
}