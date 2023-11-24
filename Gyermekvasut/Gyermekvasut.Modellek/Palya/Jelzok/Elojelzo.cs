using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya.Jelzok;

public class Elojelzo : Jelzo
{
    public ElojelzoTipus Tipus { get; }
    private Sebessegjelzes _elojelzes;
    public Sebessegjelzes Elojelzes
    {
        get => _elojelzes;
        private set
        {
            if (value != _elojelzes)
            {
                _elojelzes = value;
                OnJelzesChanged();
            }
        }
    }
    
    public Elojelzo(string nev, Irany irany, JelzoForma forma, ElojelzoTipus tipus)
        : base(nev, irany, forma)
    {
        Tipus = tipus;
    }
}

public enum ElojelzoTipus
{
    KetFogalmu = 2,
    HaromFogalmu = 3
}
