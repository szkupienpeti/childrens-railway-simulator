using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Modellek.Palya;

public class Vagany : Szakasz
{
    public AllomasNev AllomasNev { get; }
    private bool _menesztes = false;
    public bool Menesztes
    {
        get => _menesztes;
        private set
        {
            if (value != _menesztes)
            {
                _menesztes = value;
                OnMenesztesChanged();
            }
        }
    }
    public event EventHandler? MenesztesChanged;
    public event EventHandler<VonatErkezettEventArgs>? VonatErkezett;

    public Vagany(string nev, AllomasNev allomasNev) : base(nev)
    {
        AllomasNev = allomasNev;
    }

    public Vagany(string nev, AllomasNev allomasNev, int hossz) : base(nev, hossz)
    {
        AllomasNev = allomasNev;
    }

    public void Meneszt()
    {
        Menesztes = true;
    }

    public override void Felszabadit(Szerelveny szerelveny)
    {
        base.Felszabadit(szerelveny);
        Menesztes = false;
    }

    private void OnMenesztesChanged()
    {
        MenesztesChanged?.Invoke(this, EventArgs.Empty);
    }

    protected override void OnSzerelvenyChanged(Szerelveny? elozoSzerelveny)
    {
        base.OnSzerelvenyChanged(elozoSzerelveny);
        if (Szerelveny is Vonat vonat)
        {
            OnVonatErkezett(vonat);
        }
    }

    private void OnVonatErkezett(Vonat vonat)
    {
        VonatErkezett?.Invoke(this, new VonatErkezettEventArgs(this, vonat));
    }
}

public class VonatErkezettEventArgs : EventArgs
{
    public Vagany Vagany { get; set; }
    public Vonat Vonat { get; set; }
    public VonatErkezettEventArgs(Vagany vagany, Vonat vonat)
    {
        Vagany = vagany;
        Vonat = vonat;
    }
}