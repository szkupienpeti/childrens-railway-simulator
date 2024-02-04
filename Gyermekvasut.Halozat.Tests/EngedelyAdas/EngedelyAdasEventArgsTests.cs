using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.EngedelyAdas;

[TestClass]
public class EngedelyAdasEventArgsTests
{
    [TestMethod]
    public void FromGrpcEventArgs_WhenRequestHasUtolsoVonatAndTipusIsAzonosIranyu_ShouldThrow()
    {
        // Arrange
        var grpcEventArgs = new GrpcRequestEventArgs<EngedelyAdasRequest>(new EngedelyAdasRequest
        {
            Kuldo = GrpcAllomasNev.Szechenyihegy,
            Tipus = GrpcEngedelyAdasTipus.AzonosIranyu,
            UtolsoVonat = VonatTestsUtil.PAROS_VONATSZAM,
            Vonatszam = VonatTestsUtil.PARATLAN_VONATSZAM,
            Nev = TelefonTestsUtil.TEST_NEV
        });
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(() => EngedelyAdasEventArgs.FromGrpcEventArgs(grpcEventArgs));
        Assert.AreEqual("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélyadás", exception.Message);
    }

    [TestMethod]
    public void FromGrpcEventArgs_WhenRequestNoUtolsoVonatAndTipusIsEllenkezoIranyu_ShouldThrow()
    {
        // Arrange
        var grpcEventArgs = new GrpcRequestEventArgs<EngedelyAdasRequest>(new EngedelyAdasRequest
        {
            Kuldo = GrpcAllomasNev.Szechenyihegy,
            Tipus = GrpcEngedelyAdasTipus.EllenkezoIranyu,
            Vonatszam = VonatTestsUtil.PARATLAN_VONATSZAM,
            Nev = TelefonTestsUtil.TEST_NEV
        });
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(() => EngedelyAdasEventArgs.FromGrpcEventArgs(grpcEventArgs));
        Assert.AreEqual("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélyadás", exception.Message);
    }
}
