using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Biztberek.Valtozaras;
using Gyermekvasut.Halozat;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Forms;

public partial class AllomasForm : Form
{
    public HalozatiAllomas Allomas { get; }
    public ValtozarasBiztber ValtozarasBiztber { get; }
    private ValtozarasEmeltyuCsoport csoport;
    private KetfogalmuElojelzoEmeltyu<ValtozarasBiztber> elojelzoEmeltyu;
    private BejaratiJelzoEmeltyu1<ValtozarasBiztber> bejaratiJelzoEmeltyu1;
    private BejaratiJelzoEmeltyu2<ValtozarasBiztber> bejaratiJelzoEmeltyu2;
    public AllomasForm(HalozatiAllomas allomas)
    {
        Allomas = allomas;
        InitializeComponent();
        Text = Allomas.AllomasNev.Nev();
        SubscribeToEvents();
        ValtozarasBiztberFactory valtozarasBiztberFactory = new(allomas);
        ValtozarasBiztber = valtozarasBiztberFactory.Create();
        csoport = ValtozarasBiztber.EmeltyuCsoportok[Irany.KezdopontFele];
        elojelzoEmeltyu = csoport.ElojelzoEmeltyu;
        bejaratiJelzoEmeltyu1 = csoport.BejaratiJelzoEmeltyu1;
        bejaratiJelzoEmeltyu2 = csoport.BejaratiJelzoEmeltyu2;
    }

    private void SubscribeToEvents()
    {
        Allomas.CsengetesEvent += Allomas_CsengetesEvent;


        /*var i_s_allomaskoz = Allomas.Topologia.KpAllomaskoz;
        i_s_allomaskoz.SzerelvenyChanged += I_S_Allomaskoz_SzerelvenyChanged;
        Szerelveny szerelveny = new("100", Irany.Paros, i_s_allomaskoz, new Jarmu("Mk45-2001", JarmuTipus.Mk45));
        szerelveny.Tovabblep();*/
    }

    private void I_S_Allomaskoz_SzerelvenyChanged(object? sender, EventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(() => i_s.Text = (sender as Szakasz)!.Szerelveny?.Nev);
        }
        else
        {
            i_s.Text = (sender as Szakasz)!.Szerelveny?.Nev;
        }
    }

    private void Allomas_CsengetesEvent(object? sender, CsengetesEventArgs e)
    {
        string csengetesek = "";
        foreach (var csengetes in e.Csengetesek)
        {
            switch (csengetes)
            {
                case Modellek.Telefon.Csengetes.Rovid:
                    csengetesek += ".";
                    break;
                case Modellek.Telefon.Csengetes.Hosszu:
                    csengetesek += "—";
                    break;
            }
        }
        MessageBox.Show($"{Allomas.AllomasNev.Nev()}: Csengettek - from {e.Kuldo}: {csengetesek}");
    }

    private void button1_Click(object sender, EventArgs e)
    {
        Allomas.Csenget(Irany.KezdopontFele, new List<Modellek.Telefon.Csengetes> { Modellek.Telefon.Csengetes.Hosszu });
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Allomas.Csenget(Irany.VegpontFele, new List<Modellek.Telefon.Csengetes> { Modellek.Telefon.Csengetes.Hosszu, Modellek.Telefon.Csengetes.Hosszu });
    }

    private void button3_Click(object sender, EventArgs e)
    {
        MessageBox.Show("Test");
    }

    private void button4_Click(object sender, EventArgs e)
    {
        var result = elojelzoEmeltyu.AllitasiKiserlet(ValtozarasBiztber);
        MessageBox.Show(result.ToString());
    }

    private void button5_Click(object sender, EventArgs e)
    {
        var result = bejaratiJelzoEmeltyu1.AllitasiKiserlet(ValtozarasBiztber);
        MessageBox.Show(result.ToString());
    }

    private void button6_Click(object sender, EventArgs e)
    {
        var result = bejaratiJelzoEmeltyu2.AllitasiKiserlet(ValtozarasBiztber);
        MessageBox.Show(result.ToString());
    }

    private void button7_Click(object sender, EventArgs e)
    {
        csoport.ValtozarKulcsTarolo.ValtozarKulcsBetesz(ValtoAllas.Egyenes);
    }
}