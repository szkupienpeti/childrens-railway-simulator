using Gyermekvasut.Modellek.Telefon;
using Gyermekvasut.Modellek;

namespace Gyermekvasut.Tests.Util;

public static class TelefonTestsUtil
{
    public static readonly List<Csengetes> EGY_HOSSZU = new() { Csengetes.Hosszu };
    public static readonly List<Csengetes> KET_HOSSZU = new() { Csengetes.Hosszu, Csengetes.Hosszu };

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
