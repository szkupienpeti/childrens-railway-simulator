using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;

namespace Gyermekvasut.Tests.Util;

public static class DynamicTestDataUtil
{
    public static IEnumerable<object[]> AllomasNevSzomszedIranyok
        => EnumValues<AllomasNev>()
            .SelectMany(a => a.SzomszedIranyok()
                              .Select(szi => new object[] { a, szi }));

    public static IEnumerable<object[]> AllomasNevekKezdopontiSzomszeddal
        => EnumValueRowsExcept(AllomasNev.Szechenyihegy);
    
    public static IEnumerable<object[]> AllomasNevekVegpontiSzomszeddal
        => EnumValueRowsExcept(AllomasNev.Huvosvolgy);

    public static IEnumerable<object[]> AllomasNevValues
        => EnumValueRows<AllomasNev>();

    public static IEnumerable<object[]> ValtoAllasValues
        => EnumValueRows<ValtoAllas>();

    private static IEnumerable<object[]> EnumValueRowsExcept<T>(T except)
        where T : Enum
    {
        var values = EnumValues<T>();
        values.Remove(except);
        return values.Select(TestDataRowSelector);
    }

    private static IEnumerable<object[]> EnumValueRows<T>()
            where T : Enum
        => EnumValues<T>()
            .Select(TestDataRowSelector);

    public static List<T> EnumValues<T>()
            where T : Enum
        => Enum.GetValues(typeof(T))
            .Cast<T>()
            .ToList();

    private static object[] TestDataRowSelector<T>(T value)
            where T : Enum
        => new object[] { value };
}
