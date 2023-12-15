using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya.Jelzok;

public class Fojelzo : Jelzo
{
    public FojelzoRendeltetes Rendeltetes { get; }
    private Sebessegjelzes _jelzes;
    public Sebessegjelzes Jelzes
    {
        get => _jelzes;
        set
        {
            if (value != _jelzes)
            {
                _jelzes = value;
                OnJelzesChanged();
            }
        }
    }

    private Sebessegjelzes? _elojelzes;
    public Sebessegjelzes? Elojelzes
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
    
    public Fojelzo(string nev, Irany irany, JelzoForma forma, FojelzoRendeltetes rendeltetes)
        : base(nev, irany, forma)
    {
        Rendeltetes = rendeltetes;
    }
}

public enum FojelzoRendeltetes
{
    Bejarati = 1,
    Kijarati = 2
}
