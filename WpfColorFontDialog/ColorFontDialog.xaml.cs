using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace WpfColorFontDialog
{
    /// <summary>
    /// Interaction logic for ColorFontDialog.xaml
    /// </summary>
    public partial class ColorFontDialog
    {
        public FontInfo Font { get; set; }

        private int[] _defaultFontSizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 96 };
        private int[] _fontSizes;
        public int[] FontSizes
        {
            get => _fontSizes ?? _defaultFontSizes;
            set => _fontSizes = value;
        }
        public ColorFontDialog(bool previewFontInFontList = true, bool allowArbitraryFontSizes = true, bool showColorPicker = true)
        {
            InitializeComponent();
          //  I18NUtil.SetLanguage(Resources);
            colorFontChooser.PreviewFontInFontList = previewFontInFontList;
            colorFontChooser.AllowArbitraryFontSizes = allowArbitraryFontSizes;
            colorFontChooser.ShowColorPicker = showColorPicker;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Font = colorFontChooser.SelectedFont;
            DialogResult = true;
        }

        private void SyncFontColor()
        {
            int colorIdx = AvailableColors.GetFontColorIndex(Font.Color);
            colorFontChooser.colorPicker.superCombo.SelectedIndex = colorIdx;
            colorFontChooser.txtSampleText.Foreground = Font.Color.Brush;
            colorFontChooser.colorPicker.superCombo.BringIntoView();
        }
        private void SyncFontName()
        {
            string fontFamilyName = Font.Family.Source;
            bool foundMatch = false;
            int idx = 0;
            foreach (object item in colorFontChooser.lstFamily.Items)
            {
                if (fontFamilyName == item.ToString())
                {
                    foundMatch = true;
                    break;
                }
                idx++;
            }

            if (!foundMatch)
            {
                idx = 0;
            }
            colorFontChooser.lstFamily.SelectedIndex = idx;
            colorFontChooser.lstFamily.ScrollIntoView(colorFontChooser.lstFamily.Items[idx]);
        }

        private void SyncFontSize()
        {
            double fontSize = Font.Size;
            colorFontChooser.lstFontSizes.ItemsSource = FontSizes;
            colorFontChooser.tbFontSize.Text = fontSize.ToString();
        }

        private void SyncFontTypeface()
        {
            string fontTypeFaceSb = FontInfo.TypefaceToString(Font.Typeface);
            int idx = colorFontChooser.lstTypefaces.Items.Cast<object>().TakeWhile(item => fontTypeFaceSb != FontInfo.TypefaceToString(item as FamilyTypeface)).Count();
            colorFontChooser.lstTypefaces.SelectedIndex = idx;
            colorFontChooser.lstTypefaces.ScrollIntoView(colorFontChooser.lstTypefaces.SelectedItem);
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            SyncFontColor();
            SyncFontName();
            SyncFontSize();
            SyncFontTypeface();
        }
    }
}
