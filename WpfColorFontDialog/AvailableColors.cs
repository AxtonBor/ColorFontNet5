using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace WpfColorFontDialog
{
    internal class AvailableColors : List<FontColor>
    {
        public AvailableColors()
        {
            Init();
        }

        public static FontColor GetFontColor(SolidColorBrush b) => new AvailableColors().GetFontColorByBrush(b);

        public static FontColor GetFontColor(string name) => new AvailableColors().GetFontColorByName(name);

        public static FontColor GetFontColor(Color c) => GetFontColor(new SolidColorBrush(c));

        public FontColor GetFontColorByBrush(SolidColorBrush b) => this.FirstOrDefault(brush => brush.Brush.Color.Equals(b.Color));

        public FontColor GetFontColorByName(string name) => this.FirstOrDefault(b => b.Name == name);

        public static int GetFontColorIndex(FontColor c)
        {
            AvailableColors brushList = new AvailableColors();
            int idx = 0;
            SolidColorBrush colorBrush = c.Brush;
            foreach (FontColor brush in brushList)
            {
                if (brush.Brush.Color.Equals(colorBrush.Color))
                {
                    break;
                }
                idx++;
            }
            return idx;
        }


        private void Init()
        {
            PropertyInfo[] properties = typeof(Colors).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (PropertyInfo prop in properties)
            {
                string name = prop.Name;
                SolidColorBrush brush = new((Color)prop.GetValue(null, null));
                Add(new FontColor(name, brush));
            }
        }
    }
}