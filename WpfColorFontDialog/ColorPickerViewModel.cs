using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace WpfColorFontDialog
{
	internal class ColorPickerViewModel : INotifyPropertyChanged
	{
        private FontColor selectedFontColor;

		public ReadOnlyCollection<FontColor> FontColors { get; }

        public FontColor SelectedFontColor
		{
			get => selectedFontColor;
            set
            {
                if (selectedFontColor.Equals(value)) return;
                selectedFontColor = value;
                OnPropertyChanged("SelectedFontColor");
            }
		}

		public ColorPickerViewModel()
		{
			selectedFontColor = AvailableColors.GetFontColor(Colors.Black);
			FontColors = new ReadOnlyCollection<FontColor>(new AvailableColors());
		}

		private void OnPropertyChanged(string propertyName)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

		public event PropertyChangedEventHandler PropertyChanged;
	}
}