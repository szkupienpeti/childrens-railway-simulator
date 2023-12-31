using Gyermekvasut.Grpc.Client;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class MockHalozatiAllomasTestBase : HalozatiAllomasTestBase
{
    private static readonly string MOCK_ADDRESS = "https://0.0.0.0:0";

    private Mock<IGrpcAllomasServer>? _grpcServerMock;
    protected Mock<IGrpcAllomasServer> GrpcServerMock => _grpcServerMock!;

    private Mock<GrpcAllomasClient>? _kpClientMock;
    protected Mock<GrpcAllomasClient> KpClientMock => _kpClientMock!;

    private Mock<GrpcAllomasClient>? _vpClientMock;
    protected Mock<GrpcAllomasClient> VpClientMock => _vpClientMock!;

    protected virtual void MockAllomasFelepit(AllomasNev allomasNev)
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
}
