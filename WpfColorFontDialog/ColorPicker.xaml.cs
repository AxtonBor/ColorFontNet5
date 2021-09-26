using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfColorFontDialog
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker
    {
        private ColorPickerViewModel viewModel;

        public static readonly RoutedEvent ColorChangedEvent;

        public static readonly DependencyProperty SelectedColorProperty;

        public FontColor SelectedColor
        {
            get => (FontColor)GetValue(SelectedColorProperty) ?? AvailableColors.GetFontColor("Black");
            set
            {
                viewModel.SelectedFontColor = value;
                SetValue(SelectedColorProperty, value);
            }
        }

        static ColorPicker()
        {
            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorPicker));
            SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(FontColor), typeof(ColorPicker), new UIPropertyMetadata(null));
        }
        public ColorPicker()
        {
            InitializeComponent();
            viewModel = new ColorPickerViewModel();
            DataContext = viewModel;
        }
        private void RaiseColorChangedEvent()
        {
            RaiseEvent(new RoutedEventArgs(ColorChangedEvent));
        }

        private void superCombo_DropDownClosed(object sender, EventArgs e)
        {
            SetValue(SelectedColorProperty, viewModel.SelectedFontColor);
            RaiseColorChangedEvent();
        }

        private void superCombo_Loaded(object sender, RoutedEventArgs e)
        {
            SetValue(SelectedColorProperty, viewModel.SelectedFontColor);
        }

        public event RoutedEventHandler ColorChanged
        {
            add => AddHandler(ColorChangedEvent, value);
            remove => RemoveHandler(ColorChangedEvent, value);
        }
    }
}
