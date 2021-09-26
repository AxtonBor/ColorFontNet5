using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfColorFontDialog
{
    public class FontInfo
    {
        public SolidColorBrush BrushColor { get; set;}

        public FontColor Color => AvailableColors.GetFontColor(BrushColor);

        public FontFamily Family { get; set; }

        public double Size { get; set; }

        public FontStretch Stretch { get; set; }

        public FontStyle Style { get; set; }

        public FamilyTypeface Typeface => new()
        {
            Stretch = Stretch,
            Weight = Weight,
            Style = Style
        };

        public FontWeight Weight { get; set; }

        public static void ApplyFont(Control control, FontInfo font)
        {
            control.FontFamily = font.Family;
            control.FontSize = font.Size;
            control.FontStyle = font.Style;
            control.FontStretch = font.Stretch;
            control.FontWeight = font.Weight;
            control.Foreground = font.BrushColor;
        }

        public static FontInfo GetControlFont(Control control)
        {
            return new()
            {
                Family = control.FontFamily,
                Size = control.FontSize,
                Style = control.FontStyle,
                Stretch = control.FontStretch,
                Weight = control.FontWeight,
                BrushColor = (SolidColorBrush)control.Foreground
            };
        }

        public static string TypefaceToString(FamilyTypeface ttf)
        {
            return new StringBuilder(ttf.Stretch.ToString())
            .Append('-')
            .Append(ttf.Weight.ToString())
            .Append('-')
            .Append(ttf.Style.ToString()).ToString();
        }
    }
}