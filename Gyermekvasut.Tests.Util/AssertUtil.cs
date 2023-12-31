using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Tests.Util;

public static class AssertUtil
{
    public static void AssertAreVonatokEqual(Vonat expected, Vonat actual)
    {
        AssertAreFieldsEqual(expected, actual, v => v.Nev);
        AssertAreFieldsEqual(expected, actual, v => v.SzerelvenyNev);
        AssertAreFieldsEqual(expected, actual, v => v.Irany);
        AssertAreFieldsEqual(expected, actual, v => v.VonatIrany);
        AssertAreFieldsEqual(expected, actual, v => v.Hossz);
        AssertAreFieldsEqual(expected, actual, v => v.MaxSebesseg);
        AssertAreFieldsEqual(expected, actual, v => v.Megszuntetve);
        AssertAreFieldsEqual(expected, actual, v => v.VeglegFeloszlott);
        AssertAreFieldsEqual(expected, actual, v => v.Koruljar);
        Assert.AreEqual(expected.AktualisMenetrend, actual.AktualisMenetrend);
        CollectionAssert.AreEqual(expected.Menetrendek, actual.Menetrendek);
        CollectionAssert.AreEqual(expected.Jarmuvek, actual.Jarmuvek);
    }

    private static void AssertAreFieldsEqual<T, TValue>(T expected, T actual, Func<T, TValue> field)
        => Assert.AreEqual(field(expected), field(actual));
}
