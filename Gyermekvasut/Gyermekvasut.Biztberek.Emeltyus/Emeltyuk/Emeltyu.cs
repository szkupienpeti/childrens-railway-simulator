namespace Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;

public abstract class Emeltyu<TAllitasiKiserletVisitor>
    where TAllitasiKiserletVisitor : EmeltyusAllitasiKiserletVisitor<TAllitasiKiserletVisitor>
{
    public string Nev { get; }
    private EmeltyuAllas _allas;
    public EmeltyuAllas Allas
    {
        get => _allas;
        protected set
        {
            if (value != _allas)
            {
                _allas = value;
                OnAllasChanged();
            }
        }
    }
    
    public event EventHandler? AllasChanged;

    public Emeltyu(string nev, EmeltyuAllas alapAllas)
    {
        Nev = nev;
        _allas = alapAllas; // Do not invoke event
    }

    public EmeltyuAllitasEredmeny AllitasiKiserlet(TAllitasiKiserletVisitor biztber)
    {
        EmeltyuAllitasEredmeny biztberEredmeny = BiztberAllitasiKiserlet(biztber);
        if (biztberEredmeny == EmeltyuAllitasEredmeny.Allithato)
        {
            Allit();
            return EmeltyuAllitasEredmeny.Atallitva;
        }
        else
        {
            return biztberEredmeny;
        }
    }

    protected abstract EmeltyuAllitasEredmeny BiztberAllitasiKiserlet(TAllitasiKiserletVisitor biztber);

    private void Allit()
    {
        Allas = Allas.Allit();
        KulsoteriObjektumAllit();
    }

    protected abstract void KulsoteriObjektumAllit();

    private void OnAllasChanged()
    {
        AllasChanged?.Invoke(this, EventArgs.Empty);
    }
}
