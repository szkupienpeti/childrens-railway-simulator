using Gyermekvasut.Halozat;
using Gyermekvasut.Modellek.AllomasNS;

namespace Gyermekvasut.Forms;

public partial class AllomasValaszto : Form
{
    Dictionary<HalozatiAllomas, AllomasForm> allomasFormok = new();
    public AllomasValaszto(List<HalozatiAllomas> allomasok)
    {
        InitializeComponent();
        int i = 0;
        foreach (var allomas in allomasok)
        {
            AllomasForm allomasForm = new(allomas);
            allomasFormok[allomas] = allomasForm;
            AllomasButton allomasButton = new(allomasForm);
            Controls.Add(allomasButton);
            allomasButton.Location = new Point(10 + i * 50, 10);
            i++;
        }
    }
}

class AllomasButton : Button
{
    public AllomasForm AllomasForm { get; }
    public AllomasButton(AllomasForm allomasForm)
    {
        AllomasForm = allomasForm;
        Text = AllomasForm.Allomas.Nev.Nev();
        Click += AllomasButton_Click;
    }

    private void AllomasButton_Click(object? sender, EventArgs e)
    {
        AllomasForm.Show();
    }
}
