using System.Windows.Media;

namespace WpfColorFontDialog
{
    public class FontColor
    {
        public SolidColorBrush Brush { get; set; }

        public string Name { get; set; }

        public FontColor(string name, SolidColorBrush brush)
        {
            Name = name;
            Brush = brush;
        }

        public override bool Equals(object obj)
        {
            if (obj is not FontColor p || Name != p.Name)
            {
                return false;
            }
            return Brush.Equals(p.Brush);
        }

        public bool Equals(FontColor p)
        {
            return p != null && Name == p.Name && Brush.Equals(p.Brush);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string[] name = { "FontColor [Color=", Name, ", ", Brush.ToString(), "]" };
            return string.Concat(name);
        }
    }
}