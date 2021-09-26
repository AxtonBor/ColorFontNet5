using System.Windows;
using System.Windows.Media;

namespace WpfColorFont.Logic.Modelos
{
    public class lFamiliaSelecionada
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public Brush Color { get; set; } = Brushes.Black;
        public FontStyle Style { get; set; } = FontStyles.Normal;
    }
}
