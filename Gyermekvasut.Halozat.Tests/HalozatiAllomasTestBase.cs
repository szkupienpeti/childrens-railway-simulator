using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;
using Gyermekvasut.Tests.Util;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class HalozatiAllomasTestBase<TGrpcServer>
    where TGrpcServer : class, IGrpcAllomasServer
{
    protected HalozatiAllomas? _allomas;
    protected HalozatiAllomas Allomas => _allomas!;

    private Mock<TGrpcServer>? _grpcServerMock;
    protected Mock<TGrpcServer> GrpcServerMock => _grpcServerMock!;

    private Mock<GrpcAllomasClient>? _kpClientMock;
    protected Mock<GrpcAllomasClient> KpClientMock => _kpClientMock!;

    private Mock<GrpcAllomasClient>? _vpClientMock;
    protected Mock<GrpcAllomasClient> VpClientMock => _vpClientMock!;

    [TestCleanup]
    public virtual void TestCleanup()
    {
        StopIfNotNull(_allomas);
    }

    protected static void StopIfNotNull(HalozatiAllomas? allomas)
    {
        allomas?.Stop();
    }

    protected Vonat CreateInduloTestVonatAllomaskozben(Irany allomasOldal, string? vonatszam = null)
        => CreateTestVonatAllomaskozben(allomasOldal, allomasOldal, vonatszam);

    protected Vonat CreateErkezoTestVonatAllomaskozben(Irany allomasOldal, string? vonatszam = null)
        => CreateTestVonatAllomaskozben(allomasOldal, allomasOldal.Fordit(), vonatszam);

    private Vonat CreateTestVonatAllomaskozben(Irany allomasOldal, Irany vonatIrany, string? vonatszam = null)
    {
        var allomaskoz = Allomas.Topologia.Allomaskozok[allomasOldal]!;
        var vonat = VonatTestsUtil.CreateTestVonat(vonatIrany, allomaskoz, vonatszam);
        return vonat;
    }

    protected AllomasNev GetSzomszedAllomasNev(Irany irany)
        => Allomas.AllomasNev.Szomszed(irany)!.Value;

    protected Mock<GrpcAllomasClient> GetMockClient(Irany irany)
        => irany switch
        {
            Irany.KezdopontFele => KpClientMock,
            Irany.VegpontFele => VpClientMock,
            _ => throw new NotImplementedException()
        };

    protected static IConfiguration BuildTestConfig(string configFile)
    {
        var configBuilder = new HalozatConfigurationBuilder();
        configBuilder.AddJsonFile(configFile);
        return configBuilder.Build();
    }

    protected void BuildAllomasFromServerMock(IConfiguration config, AllomasNev allomasNev,
        Mock<TGrpcServer> serverMock, bool callBase)
    {
        _grpcServerMock = serverMock;
        var clientFactory = new MockGrpcAllomasClientFactory(config);
        _kpClientMock = clientFactory.CreateOptional(allomasNev.KpSzomszed(), callBase);
        _vpClientMock = clientFactory.CreateOptional(allomasNev.VpSzomszed(), callBase);
        _allomas = new(allomasNev, GrpcServerMock.Object, KpClientMock?.Object, VpClientMock?.Object);
    }
}
