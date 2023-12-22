using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya.Jelzok;

public class Ismetlojelzo : Jelzo
{
    private IsmeteltJelzes _jelzes;
    public IsmeteltJelzes Jelzes
    {
        get => _jelzes;
        private set
        {
            if (value != _jelzes)
            {
                _jelzes = value;
                OnJelzesChanged();
            }
        }
    }

    public Ismetlojelzo(string nev, Irany irany, Szelvenyszam szelvenyszam)
        : base(nev, irany, JelzoForma.FenyJelzo, szelvenyszam)
    { }
}

public enum IsmeteltJelzes
{
    Megallj = 0,
    TovabbhaladastEngedelyezo = 1
}
