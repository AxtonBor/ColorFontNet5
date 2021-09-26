using System.Windows;
using WpfColorFontV2;

namespace TestProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ColorFontChooser colorFontChooser = new()
            {
                FamiliaSelecionada =
                {
                    Name = TextBlockSample.FontFamily.ToString(),
                    Size = TextBlockSample.FontSize,
                    Color2 = TextBlockSample.Foreground,
                    Style =  TextBlockSample.FontStyle
                }
            };
            colorFontChooser.ShowDialog();
            if (colorFontChooser.OK)
            {
                TextBlockSample.FontFamily =  new System.Windows.Media.FontFamily(colorFontChooser.FamiliaSelecionada.Name);
                TextBlockSample.FontSize = colorFontChooser.FamiliaSelecionada.Size;
                TextBlockSample.Foreground = colorFontChooser.FamiliaSelecionada.Color2;
               // TextBlockSample.FontStyle = colorFontChooser.Style;
            }
        }


/*
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ColorFontDialog dialog = new()
            {
                Owner = this,
                Font = FontInfo.GetControlFont(TextBlockSample)
            };

            //dialog.FontSizes = new int[] { 10, 12, 14, 16, 18 };
            if (dialog.ShowDialog() == true)
            {
                FontInfo font = dialog.Font;
                if (font != null)
                {
                    FontInfo.ApplyFont(TextBlockSample, font);
                }
            }
        }*/

    }
}
