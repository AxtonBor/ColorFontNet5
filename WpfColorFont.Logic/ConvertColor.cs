using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfColorFont.Logic
{
    internal class ConvertColor
    {
        public SolidColorBrush Convert(string codigoColor)
        {
            Color color = (Color)ColorConverter.ConvertFromString(codigoColor);
            return new SolidColorBrush(color);
        }
    }
}
