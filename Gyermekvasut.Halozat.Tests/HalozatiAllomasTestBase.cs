using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Modellek.VonatNS;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class HalozatiAllomasTestBase
{
    protected static readonly List<Csengetes> EGY_HOSSZU = new() { Csengetes.Hosszu };
    protected static readonly List<Csengetes> KET_HOSSZU = new() { Csengetes.Hosszu, Csengetes.Hosszu };

    private static readonly string PARATLAN_VONATSZAM = "TEST_1";
    private static readonly string PAROS_VONATSZAM = "TEST_2";
    private static readonly string GEP_NEV = "TEST_Mk45";
    private static readonly Jarmu[] SZERELVENY = new Jarmu[] { new(GEP_NEV, JarmuTipus.Mk45) };
    protected static Dictionary<Irany, TestVonatInfo> VONAT_INFOS = new()
    {
        { Irany.KezdopontFele, new(PARATLAN_VONATSZAM, VonatIrany.Paratlan) },
        { Irany.VegpontFele, new(PAROS_VONATSZAM, VonatIrany.Paros) },
    };

    private static readonly string MOCK_ADDRESS = "https://0.0.0.0:0";

    private HalozatiAllomasFactory? _allomasFactory;
    protected HalozatiAllomasFactory AllomasFactory => _allomasFactory!;

    private HalozatiAllomas? _allomas;
    protected HalozatiAllomas Allomas => _allomas!;

    private Mock<IGrpcAllomasServer>? _grpcServerMock;
    protected Mock<IGrpcAllomasServer> GrpcServerMock => _grpcServerMock!;

    private Mock<GrpcAllomasClient>? _kpClientMock;
    protected Mock<GrpcAllomasClient> KpClientMock => _kpClientMock!;

    private Mock<GrpcAllomasClient>? _vpClientMock;
    protected Mock<GrpcAllomasClient> VpClientMock => _vpClientMock!;

    protected void AllomasFelepit(AllomasNev allomasNev)
    {
        _allomasFactory = new();
        _allomas = AllomasFactory.Create(allomasNev);
    }

    protected void MockAllomasFelepit(AllomasNev allomasNev)
    {
        _grpcServerMock = new Mock<IGrpcAllomasServer>();
        _kpClientMock = new Mock<GrpcAllomasClient>(MOCK_ADDRESS);
        _vpClientMock = new Mock<GrpcAllomasClient>(MOCK_ADDRESS);
        _allomas = new(allomasNev, _grpcServerMock.Object, _kpClientMock.Object, _vpClientMock.Object);
    }

    protected Mock<GrpcAllomasClient> GetMockSzomszedClient(Irany irany)
        => irany switch
        {
            Irany.KezdopontFele => KpClientMock,
            Irany.VegpontFele => VpClientMock,
            _ => throw new NotImplementedException()
        };

    [TestCleanup]
    public virtual void TestCleanup()
    {
        StopIfNotNull(_allomas);
    }

    protected static void StopIfNotNull(HalozatiAllomas? allomas)
    {
        allomas?.Stop();
    }

    protected Vonat CreateTestVonatAllomaskozben(Irany irany)
    {
        var allomaskoz = Allomas.Topologia.Allomaskozok[irany]!;
        return CreateTestVonat(irany, allomaskoz);
    }

    protected static Vonat CreateTestVonat(Irany irany, Szakasz allomaskoz)
    {
        var vonatInfo = VONAT_INFOS[irany];
        var menetrendek = new[] { vonatInfo.Menetrend };
        var vonat = new Vonat(vonatInfo.Vonatszam, irany, SZERELVENY, menetrendek);
        vonat.Lehelyez(allomaskoz);
        return vonat;
    }
}
