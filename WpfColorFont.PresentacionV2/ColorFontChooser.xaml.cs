using System;
using System.Windows;
using WpfColorFont.Logic.Modelos;
using WpfColorFont.Logic.Vistas;

namespace WpfColorFontV2
{
    /// <summary>
    /// Interaction logic for ColorFontChooser.xaml
    /// </summary>
    public partial class ColorFontChooser : Window
    {

        private viewFont vista;

        public lFamiliaSelecionada FamiliaSelecionada
        {
            get => (lFamiliaSelecionada)GetValue(FamiliaSelecionadaProperty);
            set => SetValue(FamiliaSelecionadaProperty, value);
        }

        public static readonly DependencyProperty FamiliaSelecionadaProperty = DependencyProperty.RegisterAttached("FamiliaSelecionada", typeof(lFamiliaSelecionada), typeof(ColorFontChooser), new UIPropertyMetadata(new lFamiliaSelecionada()));

        public bool OK
        {
            get => (bool)GetValue(OkProperty);
            set => SetValue(OkProperty, value);
        }

        public static readonly DependencyProperty OkProperty = DependencyProperty.RegisterAttached("OK", typeof(bool), typeof(ColorFontChooser), new UIPropertyMetadata(false));


        public ColorFontChooser()
        {
            InitializeComponent();
            vista = (viewFont)DataContext;
            vista.FamiliaSelecionada = FamiliaSelecionada;
            vista.CloseAction ??= Close;
        }

        private void ColorFontChooserControl_Closed(object sender, EventArgs e)
        {
            OK = vista.OK;
            if (OK)
            {
                FamiliaSelecionada = vista.FamiliaSelecionada;
            }
        }
    }
}
