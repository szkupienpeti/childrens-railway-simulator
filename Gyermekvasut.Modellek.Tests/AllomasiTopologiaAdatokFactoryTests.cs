using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Topologia;
using Gyermekvasut.Modellek.Topologia.ObjektumSzerepek;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Modellek.Tests;

[TestClass]
public class AllomasiTopologiaAdatokFactoryTests : SzimulaciosTestBase
{
    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevValues), typeof(DynamicTestDataUtil))]
    public void Create_MindenAllomas_AllomaskozHosszokEgyeznek(AllomasNev allomasNev)
    {
        // Arrange and act
        var adatok = AllomasiTopologiaAdatokFactory.Create(allomasNev);
        // Assert
        foreach (var irany in Enum.GetValues<Irany>())
        {
            AllomasNev? szomszedAllomasNev = allomasNev.Szomszed(irany);
            if (szomszedAllomasNev.HasValue)
            {
                var szomszedAdatok = AllomasiTopologiaAdatokFactory.Create(szomszedAllomasNev!.Value);
                AssertAllomaskozEgyezes(adatok, irany, szomszedAdatok);
            }
        }
    }

    private static void AssertAllomaskozEgyezes(AllomasiTopologiaAdatok adatok,
        Irany szomszedIrany, AllomasiTopologiaAdatok szomszedAdatok)
    {
        // Elõjelzõk távolsága
        var ejSzelveny = GetElojelzoSzelvenyOsszMeter(adatok, szomszedIrany);
        var szomszedEjSzelveny = GetElojelzoSzelvenyOsszMeter(szomszedAdatok, szomszedIrany.Fordit());
        var iranySzorzo = szomszedIrany == Irany.KezdopontFele ? 1 : -1;
        var ejTavolsag = iranySzorzo * (ejSzelveny - szomszedEjSzelveny);
        // Állomásközök
        var oldalAdat = adatok.AllomasOldalAdatok[szomszedIrany];
        Assert.AreEqual(ejTavolsag, oldalAdat.AllomaskozHossz);
        var szomszedOldalAdat = szomszedAdatok.AllomasOldalAdatok[szomszedIrany.Fordit()];
        Assert.AreEqual(ejTavolsag, szomszedOldalAdat.AllomaskozHossz);
    }

    private static int GetElojelzoSzelvenyOsszMeter(AllomasiTopologiaAdatok adatok, Irany allomasOldal)
    {
        var bejSzerep = BejaratiJelzoSzerep.GetByOldal(allomasOldal);
        var ejSzerep = bejSzerep.Elojelzo!;
        var ejSzelvenyszam = adatok.AltalanosAllomasAdat.Szelvenyszamok[ejSzerep];
        return ejSzelvenyszam.GetOsszMeter();
    }
}