using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfColorFontDialog
{
    /// <summary>
    /// Interaction logic for ColorFontChooser.xaml
    /// </summary>
    public partial class ColorFontChooser
    {
        public FontInfo SelectedFont => new()
        {
            Family = txtSampleText.FontFamily,
            Size = txtSampleText.FontSize,
            Style = txtSampleText.FontStyle,
            Stretch = txtSampleText.FontStretch,
            Weight = txtSampleText.FontWeight,
            BrushColor = colorPicker.SelectedColor.Brush
        };

        public bool ShowColorPicker
        {
            get => (bool)GetValue(ShowColorPickerProperty);
            set => SetValue(ShowColorPickerProperty, value);
        }

        // Using a DependencyProperty as the backing store for ShowColorPicker.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowColorPickerProperty =
            DependencyProperty.Register("ShowColorPicker", typeof(bool), typeof(ColorFontChooser), new PropertyMetadata(true, ShowColorPickerPropertyCallback));


        public bool AllowArbitraryFontSizes
        {
            get => (bool)GetValue(AllowArbitraryFontSizesProperty);
            set => SetValue(AllowArbitraryFontSizesProperty, value);
        }

        // Using a DependencyProperty as the backing store for AllowArbitraryFontSizes.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowArbitraryFontSizesProperty =
            DependencyProperty.Register("AllowArbitraryFontSizes", typeof(bool), typeof(ColorFontChooser), new PropertyMetadata(true, AllowArbitraryFontSizesPropertyCallback));


        public bool PreviewFontInFontList
        {
            get => (bool)GetValue(PreviewFontInFontListProperty);
            set => SetValue(PreviewFontInFontListProperty, value);
        }

        // Using a DependencyProperty as the backing store for PreviewFontInFontList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewFontInFontListProperty =
            DependencyProperty.Register("PreviewFontInFontList", typeof(bool), typeof(ColorFontChooser), new PropertyMetadata(true, PreviewFontInFontListPropertyCallback));


        public ColorFontChooser()
        {
            InitializeComponent();
            groupBoxColorPicker.Visibility = ShowColorPicker ? Visibility.Visible : Visibility.Collapsed;
            tbFontSize.IsEnabled = AllowArbitraryFontSizes;
            lstFamily.ItemTemplate = PreviewFontInFontList ? (DataTemplate)Resources["fontFamilyData"] : (DataTemplate)Resources["fontFamilyDataWithoutPreview"];
        }
        private static void PreviewFontInFontListPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorFontChooser chooser = d as ColorFontChooser;
            if (e.NewValue == null)
                return;
            chooser.lstFamily.ItemTemplate = (bool)e.NewValue
                ? chooser.Resources["fontFamilyData"] as DataTemplate
                : chooser.Resources["fontFamilyDataWithoutPreview"] as DataTemplate;
        }
        private static void AllowArbitraryFontSizesPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorFontChooser chooser = d as ColorFontChooser;
            if (e.NewValue != null) chooser.tbFontSize.IsEnabled = (bool)e.NewValue;
        }
        private static void ShowColorPickerPropertyCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorFontChooser chooser = d as ColorFontChooser;
            if (e.NewValue != null)

                chooser.groupBoxColorPicker.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
        }

        private void colorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            txtSampleText.Foreground = colorPicker.SelectedColor.Brush;
        }

        private void lstFontSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tbFontSize != null && lstFontSizes.SelectedItem != null)
            {
                tbFontSize.Text = lstFontSizes.SelectedItem.ToString();
            }
        }

        private void tbFontSize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !TextBoxTextAllowed(e.Text);
        }

        private void tbFontSize_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string Text1 = (string)e.DataObject.GetData(typeof(string));
                if (!TextBoxTextAllowed(Text1)) e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }
        private bool TextBoxTextAllowed(string Text2)
        {
            return Array.TrueForAll(Text2.ToCharArray(),
                c => char.IsDigit(c) || char.IsControl(c));
        }

        private void tbFontSize_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbFontSize.Text.Length == 0)
            {
                if (lstFontSizes.SelectedItem == null)
                {
                    lstFontSizes.SelectedIndex = 0;
                }
                tbFontSize.Text = lstFontSizes.SelectedItem.ToString();

            }
        }

        private void tbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (int size in lstFontSizes.Items)
            {
                if (size.ToString() == tbFontSize.Text)
                {
                    lstFontSizes.SelectedItem = size;
                    lstFontSizes.ScrollIntoView(size);
                    return;
                }
            }
            lstFontSizes.SelectedItem = null;
        }
    }
}
