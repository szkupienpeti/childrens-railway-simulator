using Gyermekvasut.Halozat;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Forms;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        string allomasKodokStr = args[0];
        string[] allomasKodok = allomasKodokStr.Split(',');
        HashSet<AllomasNev> allomasNevek = new();
        List<HalozatiAllomas> allomasok = new();
        foreach (var allomasKod in allomasKodok)
        {
            AllomasNev allomasNev = AllomasNevFromKod(allomasKod);
            if (allomasNevek.Contains(allomasNev))
            {
                throw new ArgumentException($"{allomasNev.Nev()} �llom�s {allomasKod} k�dja t�bbsz�r szerepel a parancssori param�terben: {allomasKodokStr}");
            }
            allomasNevek.Add(allomasNev);
            HalozatiAllomas allomas = new(allomasNev);
            allomasok.Add(allomas);
        }
        ApplicationConfiguration.Initialize();
        Application.Run(new AllomasValaszto(allomasok));
    }

    private static AllomasNev AllomasNevFromKod(this string kod)
    {
        foreach (AllomasNev allomasNev in Enum.GetValues(typeof(AllomasNev)))
        {
            if (string.Equals(allomasNev.Kod(), kod, StringComparison.OrdinalIgnoreCase))
            {
                return allomasNev;
            }
        }
        throw new ArgumentException($"Nem tal�lhat� AllomasNev {kod} k�ddal");
    }
}