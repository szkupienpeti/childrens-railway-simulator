using Gyermekvasut.Halozat;
using Gyermekvasut.Modellek.AllomasNS;
using Gyermekvasut.Modellek.Palya;
using Gyermekvasut.Modellek.VonatNS;

namespace Gyermekvasut.Forms
{
    public partial class AllomasForm : Form
    {
        public HalozatiAllomas Allomas { get; }
        public AllomasForm(HalozatiAllomas allomas)
        {
            Allomas = allomas;
            InitializeComponent();
            Text = Allomas.Nev.Nev();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            Allomas.Csengetes += Allomas_Csengetes;


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

        private void Allomas_Csengetes(object? sender, Halozat.EventArgs.CsengetesEventArgs e)
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
            MessageBox.Show($"{Allomas.Nev.Nev()}: Csengettek - from {e.Kuldo}: {csengetesek}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Allomas.Csenget(Modellek.Telefon.HivasIrany.KezdopontFele, new List<Modellek.Telefon.Csengetes> { Modellek.Telefon.Csengetes.Hosszu });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Allomas.Csenget(Modellek.Telefon.HivasIrany.VegpontFele, new List<Modellek.Telefon.Csengetes> { Modellek.Telefon.Csengetes.Hosszu, Modellek.Telefon.Csengetes.Hosszu });
        }
    }
}