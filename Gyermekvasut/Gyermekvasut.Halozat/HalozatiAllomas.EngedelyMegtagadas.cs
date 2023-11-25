using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcEngedelyMegtagadasEvent(object? sender, GrpcEngedelyMegtagadasEventArgs grpcEventArgs)
    {
        EngedelyMegtagadasEventArgs e = EngedelyMegtagadasEventArgs.FromGrpcEventArgs(grpcEventArgs);
        EngedelyMegtagadasEvent?.Invoke(this, e);
    }

    public void EngedelytMegtagad(Irany irany, string vonatszam, string ok, int percMulva, string nev)
    {
        EngedelyMegtagadasRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonatszam = vonatszam,
            Ok = ok,
            PercMulva = percMulva,
            Nev = nev
        };
        GetSzomszedClient(irany).EngedelyMegtagadasAsync(request);
    }
}