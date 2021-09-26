# Wpf Color/Font Dialog
# Cuadro de Dialogo de Fuente y Color en Net5

Aviable en (en curso) [NuGet](http://www.nuget.org/.../)

Uso:
          

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

 
![Ejemplo](https://imgur.com/a/4wS0Swe)

