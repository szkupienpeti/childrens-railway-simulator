using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;
using System.Diagnostics;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcVonatAllomaskozbolKilepEvent(object? sender, GrpcVonatAllomaskozbolKilepEventArgs grpcEventArgs)
    {
        VonatAllomaskozbolKilepEventArgs e = VonatAllomaskozbolKilepEventArgs.FromGrpcEventArgs(grpcEventArgs);
        AllomaskozbolKilepoVonatotMegszuntet(e.Kuldo, e.Vonatszam);
        VonatAllomaskozbolKilepEvent?.Invoke(this, e);
    }

    private void AllomaskozbolKilepoVonatotMegszuntet(AllomasNev kuldo, string vonatszam)
    {
        Irany szomszedIrany = AllomasNev.GetSzomszedIrany(kuldo)!.Value;
        Szakasz allomaskoz = Topologia.Allomaskozok[szomszedIrany]!;
        Szerelveny szerelveny = allomaskoz.Szerelveny!
            ?? throw new InvalidOperationException($"Az állomásköz üres, így nem léptethető ki a(z) {vonatszam} sz. vonat");
        if (szerelveny.Nev != vonatszam)
        {
            throw new ArgumentException($"Nem az állomásköz szerelvényét ({szerelveny}) próbálja kiléptetni, hanem a(z) {vonatszam} sz. vonatot");
        }
        Trace.WriteLine($"{AllomasNev}: {allomaskoz} állomásközből kilépő vonat megszüntetése: {szerelveny}");
        szerelveny.Megszuntet();
    }

    public void VonatotAllomaskozolKileptet(Irany irany, string vonatszam)
    {
        VonatAllomaskozbolKilepRequest request = new()
        {
            Kuldo = ModelToGrpcMapper.MapAllomasNev(AllomasNev),
            Vonatszam = vonatszam
        };
        GetSzomszedClient(irany).VonatAllomaskozbolKilep(request);
    }
}