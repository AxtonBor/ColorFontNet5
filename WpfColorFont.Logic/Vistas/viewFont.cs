using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Generico;
using Generico.Vistas;
using WpfColorFont.Logic.Modelos;

namespace WpfColorFont.Logic.Vistas
{
    public class viewFont : INotifyPropertyChanged
    {
        #region Commandos
        public ICommand Inicializar { get; set; }
        public ICommandAsync LeerConAwait { get; set; }
        public ICommand EstablecerTextBox { get; set; }
        public ICommand Accept { get; set; }
        public ICommand Cancel { get; set; }


        #endregion Commandos

        #region Propiedades

        private ObservableCollection<lFont> _fontNames = new();
        private lFont _selectFontName = new();
        private lFontStyle _selectFontStyle = new();
        private ObservableCollection<lFontStyle> _fontStyles = new();
        private ICollectionView _vFontsName;
        private ICollectionView _vColors;
        private viewProgressBar _progressBar;
        private TextBox _textExample;
        private ObservableCollection<lFontSize> _fontSizes;
        private lFontSize _selectFontSize;
        private double _sizeFont;
        private ObservableCollection<lColor> _colors;
        private lColor _selectColor;
        public lFamiliaSelecionada FamiliaSelecionada { get; set; }
        public bool OK { get; set; }

        public Action CloseAction { get; set; }
        public lColor SelectColor
        {
            get => _selectColor;
            set
            {
                SetField(ref _selectColor, value);
                _textExample.Foreground = new ConvertColor().Convert(SelectColor.CodigoColor);
            }
        }

        public ObservableCollection<lColor> Colors
        {
            get => _colors;
            set => SetField(ref _colors, value);
        }

        public double SizeFont
        {
            get => _sizeFont;
            set
            {
                SetField(ref _sizeFont, value);
                if (FontSizes.All(x => x.Size != _sizeFont))
                {
                    FontSizes.Add(new lFontSize { Size = _sizeFont });
                }
                SelectFontsize = FontSizes.First(x => x.Size == _sizeFont);
            }
        }

        public lFontSize SelectFontsize
        {
            get => _selectFontSize;
            set
            {
                SetField(ref _selectFontSize, value);
                _textExample.FontSize = _selectFontSize.Size;
            }
        }

        public ObservableCollection<lFontSize> FontSizes
        {
            get => _fontSizes;
            set => SetField(ref _fontSizes, value);
        }

        public lFontStyle SelectFontStyle
        {
            get => _selectFontStyle;
            set
            {
                SetField(ref _selectFontStyle, value);
                if (SelectFontStyle != null)
                {
                    _textExample.FontStyle = mostrarTipoEnTexbox();
                }
            }
        }

        public ObservableCollection<lFontStyle> FontStylesO
        {
            get => _fontStyles;
            set => SetField(ref _fontStyles, value);
        }

        public lFont SelectFontName
        {
            get => _selectFontName;
            set
            {
                SetField(ref _selectFontName, value);
                _textExample.FontFamily = new System.Windows.Media.FontFamily(SelectFontName.FontName);
                filtrar();
                SelectFontStyle ??= FontStylesO.First();
            }
        }

        public ObservableCollection<lFont> FontNames
        {
            get => _fontNames;
            set => SetField(ref _fontNames, value);
        }

        #endregion Propiedades

        #region Comun

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            notifyPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion Comun

        #region MetodosCommandos



        private bool canExecuteLeerConAwait() => !FontNames.Any();
        private void inicializar(object l)
        {
            _progressBar = l as viewProgressBar;
        }

        private void cancel()
        {
            OK = false;
            CloseAction();
        }

        private void accept()
        {
            OK = true;
            FamiliaSelecionada.Name = SelectFontName.FontName;
            FamiliaSelecionada.Size = SelectFontsize.Size;
            FamiliaSelecionada.Color2 = new ConvertColor().Convert(SelectColor.CodigoColor);
            FamiliaSelecionada.Style = mostrarTipoEnTexbox();
            CloseAction(); 
        }

        private void inicializarControles()
        {  
            if (FamiliaSelecionada != null)
            {
                if (FontNames.Any(x => x.FontName == FamiliaSelecionada.Name))
                {
                    SelectFontName = FontNames.First(x => x.FontName == FamiliaSelecionada.Name);
                }
                SizeFont = FamiliaSelecionada.Size;
                if (Colors.Any(x => x.CodigoColor == FamiliaSelecionada.Color2.ToString()))
                {
                    SelectColor = Colors.First(x => x.CodigoColor == FamiliaSelecionada.Color2.ToString());
                }
                if (FontStylesO.Any(x => x.Tipo.ToString() == FamiliaSelecionada.Style.ToString()) )
                {
                    SelectFontStyle = FontStylesO.First(x =>  x.Tipo.ToString() == FamiliaSelecionada.ToString());
                }
            }
        }

        private void establecerTextBox(object l)
        {
            _textExample = (TextBox)l;
            inicializarControles();
        }

        private async Task leerConAwait()
        {
            FontNames = new ObservableCollection<lFont>(fillFontNames());
            _vFontsName = CollectionViewSource.GetDefaultView(FontStylesO);
            FontSizes = new ObservableCollection<lFontSize>(fillFontSizes());
            Colors = new ObservableCollection<lColor>(fillColors());
            _vColors = CollectionViewSource.GetDefaultView(Colors);
            _vColors.SortDescriptions.Add(new SortDescription("ColorDesc", ListSortDirection.Ascending));
            filtrar();
        }

        private System.Windows.FontStyle mostrarTipoEnTexbox()
        {
            return SelectFontStyle.Tipo switch
            {
                System.Drawing.FontStyle.Italic => FontStyles.Italic,
                System.Drawing.FontStyle.Regular => FontStyles.Normal,
                System.Drawing.FontStyle.Underline => FontStyles.Oblique,
                System.Drawing.FontStyle.Bold => FontStyles.Normal,
                System.Drawing.FontStyle.Strikeout => FontStyles.Normal,
                _ => FontStyles.Normal
            };
        }

        #region fuentes
        private void fillOneFontStyle(lFont font)
        {
            FontFamily myFontFamily = new(font.FontName);
            if (myFontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold))
            {
                FontStylesO.Add(new lFontStyle { Descripcion = "Bold", Tipo = System.Drawing.FontStyle.Bold, NumFont = font.NumFont });
            }
            if (myFontFamily.IsStyleAvailable(System.Drawing.FontStyle.Italic))
            {
                FontStylesO.Add(new lFontStyle { Descripcion = "Italic", Tipo = System.Drawing.FontStyle.Italic, NumFont = font.NumFont });
            }
            if (myFontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold))
            {
                FontStylesO.Add(new lFontStyle { Descripcion = "Regular", Tipo = System.Drawing.FontStyle.Regular, NumFont = font.NumFont });
            }
            if (myFontFamily.IsStyleAvailable(System.Drawing.FontStyle.Bold))
            {
                FontStylesO.Add(new lFontStyle { Descripcion = "Normal", Tipo = System.Drawing.FontStyle.Strikeout, NumFont = font.NumFont });
            }
            if (myFontFamily.IsStyleAvailable(System.Drawing.FontStyle.Underline))
            {
                FontStylesO.Add(new lFontStyle { Descripcion = "Underlined text.", Tipo = System.Drawing.FontStyle.Underline, NumFont = font.NumFont });
            }
        }

        private List<lFontSize> fillFontSizes()
        {
            int[] sizesDefault = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 96 };

            return sizesDefault.Select(t => new lFontSize {Size = t}).ToList();
        }

        private List<lFont> fillFontNames()
        {
            List<lFont> fonts = new();

            InstalledFontCollection installedFontCollection = new();

            // Get the array of FontFamily objects.
            FontFamily[] fontFamilies = installedFontCollection.Families;

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                lFont font = new() { FontName = fontFamilies[j].Name, NumFont = j };
                fonts.Add(font);
                fillOneFontStyle(font);
            }
            return fonts;
        }

        #endregion

        #region Colores

        private List<lColor> fillColors()
        {
            return (from DictionaryEntry key in Language.Language.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true) 
                where key.Key.ToString().Length >= 7 && key.Key.ToString()[..7] == "IdColor"
                select key.Value.ToString().Split(";") into descColor 
                select new lColor {ColorDesc = descColor[0], CodigoColor = descColor[1]}).ToList();
        }

        #endregion

        private void filtrar()
        {
            _vFontsName.Filter = o =>
            {
                lFontStyle skin = (lFontStyle)o;
                return skin.NumFont == SelectFontName.NumFont;
            };
        }

        #endregion MetodosCommandos

        public viewFont()
        {
            crearComandos();
        }

        /// <summary>
        /// Construye los Comandos
        /// </summary>
        private void crearComandos()
        {
            LeerConAwait = new CommandBaseAsync(param => leerConAwait(), canExecuteLeerConAwait);
            Inicializar = new CommandBase(inicializar);
            EstablecerTextBox = new CommandBase(establecerTextBox);
            Accept = new CommandBase(param => accept());
            Cancel = new CommandBase(param => cancel());
        }
    }
}
