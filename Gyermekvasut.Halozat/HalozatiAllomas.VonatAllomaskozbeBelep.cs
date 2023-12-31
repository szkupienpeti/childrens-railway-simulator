using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server.EventArgsNS;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.Palya;
using System.Diagnostics;
using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Halozat.Factory;

namespace Gyermekvasut.Halozat;

public partial class HalozatiAllomas : Allomas
{
    private void AllomasServer_GrpcVonatAllomaskozbeBelepEvent(object? sender, GrpcVonatAllomaskozbeBelepEventArgs grpcEventArgs)
    {
        VonatAllomaskozbeBelepEventArgs e = VonatAllomaskozbeBelepEventArgs.FromGrpcEventArgs(grpcEventArgs);
        IranyKonzisztenciaCheck(e.Kuldo, e.Vonat.Irany);
        BelepoVonatotAllomaskozbeLehelyez(e.Vonat);
        VonatAllomaskozbeBelepEvent?.Invoke(this, e);
    }

    private void IranyKonzisztenciaCheck(AllomasNev kuldo, Irany vonatIrany)
    {
        Irany kuldoIrany = AllomasNev.GetSzomszedIrany(kuldo)!.Value;
        if (kuldoIrany == vonatIrany)
        {
            throw new InvalidOperationException(
                $"{AllomasNev}: Állomásközbe belépő vonat irány inkonzisztencia: {kuldo} felöl, {vonatIrany} irányú vonat");
        }
    }

    private void BelepoVonatotAllomaskozbeLehelyez(Vonat vonat)
    {
        Irany allomasOldalIrany = vonat.Irany.Fordit();
        Szakasz allomaskoz = Topologia.Allomaskozok[allomasOldalIrany]!;
        Trace.WriteLine($"{AllomasNev}: Belépő {vonat} vonat lehelyezése {allomaskoz} állomásközbe");
        vonat.Lehelyez(allomaskoz);
    }

    public void VonatotAllomaskozbeBeleptet(Irany irany, Vonat vonat)
    {
        VonatAllomaskozbeBelepRequest request = GrpcRequestFactory.CreateVonatAllomaskozbeBelepRequest(AllomasNev, vonat);
        GrpcAllomasClient szomszedClient = GetSzomszedClient(irany);
        szomszedClient.VonatAllomaskozbeBelep(request);
    }
}