using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Tests.Util;

public static class DynamicTestDataUtil
{
    public static IEnumerable<object[]> AllomasNevValues
        => EnumValues<AllomasNev>();

    public static IEnumerable<object[]> ValtoAllasValues
        => EnumValues<ValtoAllas>();

    public static IEnumerable<object[]> EnumValues<T>()
        where T : Enum
    {
        foreach (var literal in Enum.GetValues(typeof(T)))
        {
            yield return new object[] { literal };
        }
    }
}
