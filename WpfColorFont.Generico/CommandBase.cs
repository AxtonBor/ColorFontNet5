using System;
using System.Windows.Input;

namespace Generico
{
    public class CommandBase : ICommand
    {
        private readonly Action<object> _execAction;

        private readonly Func<object, bool> _canExecFunc;

        private readonly Func<bool> _canExecFuncSinParametro;

        public void CanExecuteRefresh()
        {
#if NET5_01

#else

            CommandManager.InvalidateRequerySuggested();

#endif // WINDOWS_UAP
        }


        public CommandBase()
        {
        }

        public CommandBase(Action<object> execAction)
        {
            _execAction = execAction;
            _canExecFunc = null;
        }

        public CommandBase(Action<object> execAction, Func<bool> canExecFuncSinParametro)
        {
            _execAction = execAction;
            _canExecFuncSinParametro = canExecFuncSinParametro;
        }

        public CommandBase(Action<object> execAction, Func<object, bool> canExecFunc)
        {
            _execAction = execAction;
            _canExecFunc = canExecFunc;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecFunc == null && _canExecFuncSinParametro == null)
            {
                return true;
            }
            if (_canExecFuncSinParametro != null)
            {
                return _canExecFuncSinParametro.Invoke();
            }
            return _canExecFunc.Invoke(parameter);
        }

#if NET5_01
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

#else

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

#endif // WINDOWS_UAP

        public void Execute(object parameter)
        {
            _execAction?.Invoke(parameter);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}