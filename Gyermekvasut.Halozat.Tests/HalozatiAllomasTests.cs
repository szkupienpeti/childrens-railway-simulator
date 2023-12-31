using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Tests.Util;

namespace Gyermekvasut.Halozat.Tests;

[TestClass]
public class HalozatiAllomasTests : HalozatiAllomasTestBase
{
    [TestMethod]
    [DynamicData(nameof(DynamicTestDataUtil.AllomasNevValues), typeof(DynamicTestDataUtil))]
    public void Create_WhenNotCreated_ShouldCreate(AllomasNev allomasNev)
    {
        // Act
        AllomasFelepit(allomasNev);
        // Assert
        Assert.IsNotNull(Allomas);
    }

    // TODO Starting a second GrpcServers on the same port should throw an Exception

    //[TestMethod]
    //[DynamicData(nameof(DynamicTestDataUtil.AllomasNevValues), typeof(DynamicTestDataUtil))]
    //public void Create_WhenAlreadyCreated_ShouldThrow(AllomasNev allomasNev)
    //{
    //    // Arrange
    //    AllomasFelepit(allomasNev);
    //    // Act and assert
    //    Assert.ThrowsException<Exception>(() => new HalozatiAllomas(allomasNev));
    //}
}
