namespace Gyermekvasut.Modellek.Palya.Jelzok;

public class Elojelzo : Jelzo
{
    public ElojelzoTipus Tipus { get; }
    private Sebessegjelzes _elojelzes;
    public Sebessegjelzes Elojelzes
    {
        get => _elojelzes;
        set
        {
            if (value != _elojelzes)
            {
                _elojelzes = value;
                OnJelzesChanged();
            }
        }
    }
    
    public Elojelzo(string nev, Irany irany, JelzoForma forma, ElojelzoTipus tipus, Szelvenyszam szelvenyszam)
        : base(nev, irany, forma, szelvenyszam)
    {
        Tipus = tipus;
    }
}

public enum ElojelzoTipus
{
    KetFogalmu = 2,
    HaromFogalmu = 3
}
