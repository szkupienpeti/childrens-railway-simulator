using Gyermekvasut.Grpc;
using Gyermekvasut.Grpc.Server;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests.EngedelyKeres;

[TestClass]
public class EngedelyKeresEventArgsTests
{
    [TestMethod]
    public void FromGrpcEventArgs_WhenRequestHasUtolsoVonatAndTipusIsAzonosIranyu_ShouldThrow()
    {
        // Arrange
        var grpcEventArgs = new GrpcRequestEventArgs<EngedelyKeresRequest>(new EngedelyKeresRequest
        {
            Kuldo = GrpcAllomasNev.Szechenyihegy,
            Tipus = GrpcEngedelyKeresTipus.AzonosIranyuVolt,
            UtolsoVonat = VonatTestsUtil.PAROS_VONATSZAM,
            Vonatszam = VonatTestsUtil.PARATLAN_VONATSZAM,
            Ido = ModelToGrpcMapper.MapTimeOnly(TelefonTestsUtil.TEST_IDO),
            Nev = TelefonTestsUtil.TEST_NEV
        });
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(() => EngedelyKeresEventArgs.FromGrpcEventArgs(grpcEventArgs));
        Assert.AreEqual("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélykérés", exception.Message);
    }

    [DataTestMethod]
    [DataRow(GrpcEngedelyKeresTipus.EllenkezoIranyuVolt)]
    [DataRow(GrpcEngedelyKeresTipus.EllenkezoIranyuVan)]
    public void FromGrpcEventArgs_WhenRequestNoUtolsoVonatAndTipusIsEllenkezoIranyu_ShouldThrow(GrpcEngedelyKeresTipus ellenkezoIranyuEnedelyKeresTipus)
    {
        // Arrange
        var grpcEventArgs = new GrpcRequestEventArgs<EngedelyKeresRequest>(new EngedelyKeresRequest
        {
            Kuldo = GrpcAllomasNev.Szechenyihegy,
            Tipus = ellenkezoIranyuEnedelyKeresTipus,
            Vonatszam = VonatTestsUtil.PARATLAN_VONATSZAM,
            Ido = ModelToGrpcMapper.MapTimeOnly(TelefonTestsUtil.TEST_IDO),
            Nev = TelefonTestsUtil.TEST_NEV
        });
        // Act and assert
        var exception = Assert.ThrowsException<ArgumentException>(() => EngedelyKeresEventArgs.FromGrpcEventArgs(grpcEventArgs));
        Assert.AreEqual("Az utolsó vonatnak pontosan akkor kell hiányoznia, ha azonos irányú az engedélykérés", exception.Message);
    }
}
