using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Tests.Util;

public static class TelefonTestsUtil
{
    public static readonly List<Csengetes> EGY_HOSSZU = new() { Csengetes.Hosszu };
    public static readonly List<Csengetes> KET_HOSSZU = new() { Csengetes.Hosszu, Csengetes.Hosszu };

    public static readonly TimeOnly TEST_IDO = new(9, 10);
    public static readonly string TEST_NEV = "TEST_NEV";
    public static readonly string TEST_ENGEDELY_MEGTAGADAS_OK = "TEST_OK";
    public static readonly int TEST_ENGEDELY_MEGTAGADAS_PERC_MULVA = 5;

    public static List<Csengetes> GetBejovoCsengetes(Irany irany)
        => GetKimenoCsengetes(irany.Fordit());

    public static List<Csengetes> GetKimenoCsengetes(Irany irany)
        => irany switch
        {
            Irany.KezdopontFele => EGY_HOSSZU,
            Irany.VegpontFele => KET_HOSSZU,
            _ => throw new NotImplementedException()
        };

    public static List<Csengetes> GetBejovoVisszaCsengetes(Irany irany)
        => GetBejovoCsengetes(irany);

    public static List<Csengetes> GetKimenoVisszaCsengetes(Irany irany)
        => GetKimenoCsengetes(irany);
}
