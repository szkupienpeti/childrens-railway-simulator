using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.BiztberNS;
using Gyermekvasut.Modellek.Ido;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;
using Moq;

namespace Gyermekvasut.Biztberek.Valtozaras.Tests;

[TestClass]
public abstract class ValtozarasBiztberTestBase : SzimulaciosTestBase
{
    protected static readonly string VONATSZAM = "";
    protected static readonly Jarmu GEP = new("Mk45", JarmuTipus.Mk45);

    protected static IEnumerable<object[]> ValtozarasBiztberAllomasOldalakData
        => new[]
        {
            new object[] { AllomasNev.Viragvolgy, Irany.KezdopontFele },
            new object[] { AllomasNev.Viragvolgy, Irany.VegpontFele },
            new object[] { AllomasNev.Janoshegy, Irany.KezdopontFele },
            new object[] { AllomasNev.Janoshegy, Irany.VegpontFele }
        };

    protected Dictionary<Irany, Mock<ITimer>> TimerMocks { get; } = new();
    private AllomasNev? _allomasNev;
    protected AllomasNev AllomasNev => _allomasNev!.Value;
    private Allomas? _allomas;
    protected Allomas Allomas => _allomas!;
    private ValtozarasBiztber? _biztber;
    protected ValtozarasBiztber Biztber => _biztber!;

    [TestInitialize]
    public override void TestInitialize()
    {
        base.TestInitialize();
        foreach (var irany in Enum.GetValues<Irany>())
        {
            TimerMocks.Add(irany, new());
        }
    }

    protected void CreateBiztber(AllomasNev allomasNev)
    {
        _allomasNev = allomasNev;
        _allomas = new(allomasNev);
        var timerFactoryMock = new Mock<ITimerFactory>();
        timerFactoryMock.SetupSequence(f => f.Create(It.IsAny<bool>(), It.IsAny<double>()))
            .Returns(TimerMocks[Irany.KezdopontFele].Object)
            .Returns(TimerMocks[Irany.VegpontFele].Object);
        ValtozarasBiztberFactory factory = new(Allomas, timerFactoryMock.Object);
        _biztber = factory.Create();
    }

    // Vágányút-beállítás
    protected void BejaratiVaganyutBeallit(Irany irany, ValtoAllas valtoAllas)
        => VaganyutBeallit(irany, VaganyutIrany.Bejarat, valtoAllas);

    private void VaganyutBeallit(Irany irany, VaganyutIrany vaganyutIrany, ValtoAllas valtoAllas)
    {
        var valtokezelo = GetValtokezelo(irany);
        var vagany = Allomas.Topologia.LezarasiTablazat.GetVagany(valtokezelo.Valto, valtoAllas)!;
        switch (vaganyutIrany)
        {
            case VaganyutIrany.Bejarat:
                valtokezelo.BejaratElrendel(new(VaganyutIrany.Bejarat, VONATSZAM, vagany, new()));
                break;
            case VaganyutIrany.Kijarat:
                valtokezelo.KijaratElrendel(new(VaganyutIrany.Kijarat, VONATSZAM, vagany, new()));
                break;
        }
        TimerMocks[irany].Raise(t => t.Elapsed += null, EventArgs.Empty);
    }

    // Jelzőállítás
    protected void BejaratiJelzoAllit(Irany irany, ValtoAllas valtoAllas)
    {
        switch (valtoAllas)
        {
            case ValtoAllas.Egyenes:
                GetBejaratiJelzoEmeltyu1(irany).AllitasiKiserlet(Biztber);
                break;
            case ValtoAllas.Kitero:
                GetBejaratiJelzoEmeltyu2(irany).AllitasiKiserlet(Biztber);
                break;
            default:
                break;
        }
    }

    protected void ElojelzoAllit(Irany irany)
    {
        GetElojelzoEmeltyu(irany).AllitasiKiserlet(Biztber);
    }

    // Vonat ki-/behaladás
    protected void VonatBehalad(Irany irany)
    {
        var vonat = new Szerelveny(VONATSZAM, irany.Fordit(), Allomas.Topologia.Allomaskozok[irany]!, GEP);
        var valtokezelo = GetValtokezelo(irany);
        valtokezelo.GetOldoSzakaszElottiSzakasz().Elfoglal(vonat);
        valtokezelo.GetOldoSzakasz().Elfoglal(vonat);
        valtokezelo.GetOldoSzakaszElottiSzakasz().Felszabadit(vonat);
    }

    // Getterek
    private ValtozarasValtokezelo GetValtokezelo(Irany irany)
        => Biztber!.EmeltyuCsoportok[irany].Valtokezelo;

    protected KetfogalmuElojelzoEmeltyu<ValtozarasBiztber> GetElojelzoEmeltyu(Irany irany)
        => Biztber!.EmeltyuCsoportok[irany].ElojelzoEmeltyu;

    protected BejaratiJelzoEmeltyu1<ValtozarasBiztber> GetBejaratiJelzoEmeltyu1(Irany irany)
        => Biztber!.EmeltyuCsoportok[irany].BejaratiJelzoEmeltyu1;

    protected BejaratiJelzoEmeltyu2<ValtozarasBiztber> GetBejaratiJelzoEmeltyu2(Irany irany)
        => Biztber!.EmeltyuCsoportok[irany].BejaratiJelzoEmeltyu2;

    // Assertek
    protected static void AssertSikertelenAllitasEredmeny(EmeltyuAllitasEredmeny expected, EmeltyuAllitasEredmenyek eredmenyek)
    {
        Assert.AreEqual(expected, eredmenyek.BiztberEredmeny);
        Assert.AreEqual(expected, eredmenyek.AllitasEredmeny);
    }

    protected static void AssertSikeresAllitasEredmeny(EmeltyuAllitasEredmenyek eredmenyek)
    {
        Assert.AreEqual(EmeltyuAllitasEredmeny.Allithato, eredmenyek.BiztberEredmeny);
        Assert.AreEqual(EmeltyuAllitasEredmeny.Atallitva, eredmenyek.AllitasEredmeny);
    }
}

public record EmeltyuAllitasEredmenyek(EmeltyuAllitasEredmeny BiztberEredmeny, EmeltyuAllitasEredmeny AllitasEredmeny);
