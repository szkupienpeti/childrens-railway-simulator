using Gyermekvasut.Biztberek.Emeltyus.Emeltyuk;
using Gyermekvasut.Biztberek.Valtozaras;
using Gyermekvasut.Halozat;
using Gyermekvasut.Halozat.EventArgsNS;
using Gyermekvasut.Modellek;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;
using System.Text;

namespace Gyermekvasut.Forms;

public partial class AllomasForm : Form
{
    public HalozatiAllomas Allomas { get; }
    public AllomasForm(HalozatiAllomas allomas)
    {
        Allomas = allomas;
        InitializeComponent();
        Text = Allomas.AllomasNev.Nev();
        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        Allomas.CsengetesEvent += Allomas_CsengetesEvent;
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
        StringBuilder sb = new();
        foreach (var csengetes in e.Csengetesek)
        {
            switch (csengetes)
            {
                case Modellek.Telefon.Csengetes.Rovid:
                    sb.Append('.');
                    break;
                case Modellek.Telefon.Csengetes.Hosszu:
                    sb.Append('—');
                    break;
            }
        }
        string csengetesek = sb.ToString();
        MessageBox.Show($"{Allomas.AllomasNev.Nev()}: Csengettek - from {e.Kuldo}: {csengetesek}");
    }

    private void KpCsenget_Click(object sender, EventArgs e)
    {
        Allomas.Csenget(Irany.KezdopontFele, new List<Modellek.Telefon.Csengetes> { Modellek.Telefon.Csengetes.Hosszu });
    }

    private void VpCsenget_Click(object sender, EventArgs e)
    {
        Allomas.Csenget(Irany.VegpontFele, new List<Modellek.Telefon.Csengetes> { Modellek.Telefon.Csengetes.Hosszu, Modellek.Telefon.Csengetes.Hosszu });
    }
}