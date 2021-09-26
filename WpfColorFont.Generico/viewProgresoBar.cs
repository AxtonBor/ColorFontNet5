using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Generico.Vistas
{
    public class viewProgressBar : INotifyPropertyChanged
    {
        #region Propiedades

        private bool _progresoVisible;
        private int _valorMaximo;
        private int _valorActual;
        private int _valorPorCentaje;
        private int _valorMinimo;

        /// <summary>
        /// Deshabilita los botones cuando se esta procesando un perfil
        /// </summary>
        /// <value>
        /// Verdadero si no se esta procesando un perfil
        /// </value>
        public bool ProgresoVisible
        {
            get => _progresoVisible;
            set => SetField(ref _progresoVisible, value);
        }

        /// <summary>
        /// Devuelve o establece el valor mínimo que alcanzara la barra de progreso
        /// </summary>
        /// <value>
        /// El valor mínimo que alcanzara la barra de progreso
        /// </value>
        public int ValorMinimo
        {
            get => _valorMinimo;
            set => SetField(ref _valorMinimo, value);
        }

        /// <summary>
        /// Devuelve o establece el valor máximo que alcanzara la barra de progreso
        /// </summary>
        /// <value>
        /// El valor máximo que alcanzara la barra de progreso
        /// </value>
        public int ValorMaximo
        {
            get => _valorMaximo;
            set => SetField(ref _valorMaximo, value);
        }

        /// <summary>
        /// Devuelve o establece el valor actual de la barra de progreso
        /// </summary>
        /// <value>
        /// El valor actual de la barra de progreso
        /// </value>
        public int ValorActual
        {
            get => _valorActual;
            set => SetField(ref _valorActual, value);
        }

        /// <summary>
        /// Devuelve o establece el valor actual en forma de porcentaje
        /// </summary>
        /// <value>
        /// El valor actual en forma de porcentaje
        /// </value>
        public int ValorPorCentaje
        {
            get => _valorPorCentaje;
            set => SetField(ref _valorPorCentaje, value);
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

        private void notifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Comun
    }
}