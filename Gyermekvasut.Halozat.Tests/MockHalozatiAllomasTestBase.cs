using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Modellek.AllomasNS;
using Moq;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public abstract class MockHalozatiAllomasTestBase : HalozatiAllomasTestBase<IGrpcAllomasServer>
{
    private static readonly string HALOZAT_TEST_CONFIG_FILE = "gyermekvasut.halozat.settings.test.json";

    protected virtual void MockAllomasFelepit(AllomasNev allomasNev)
    {
        var testConfig = BuildTestConfig(HALOZAT_TEST_CONFIG_FILE);
        var grpcServerMock = new Mock<IGrpcAllomasServer>();
        BuildAllomasFromServerMock(testConfig, allomasNev, grpcServerMock, false);
    }
}
