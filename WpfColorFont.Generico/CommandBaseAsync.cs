using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfColorFont.Generico
{
    public class CommandBaseAsync : ICommandAsync
    {
        private readonly Func<object, Task> _execAction;

        private readonly Func<object, bool> _canExecFunc;

        private readonly Func<bool> _canExecFuncSinParametro;

        //   private bool isExecuting;

        //  public event EventHandler Started;

        //   public event EventHandler Ended;

        /*  public bool IsExecuting
          {
              get { return this.isExecuting; }
          }*/

        public CommandBaseAsync(Func<object, Task> execAction)
        {
            _execAction = execAction;
            _canExecFunc = null;
        }

        public CommandBaseAsync(Func<object, Task> execAction, Func<bool> canExecFuncSinParametro)
        {
            _execAction = execAction;
            _canExecFuncSinParametro = canExecFuncSinParametro;
        }

        public CommandBaseAsync(Func<object, Task> execAction, Func<object, bool> canExecFunc)
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
            else
            {
                if (_canExecFuncSinParametro != null)
                {
                    return _canExecFuncSinParametro.Invoke();
                }
                return _canExecFunc.Invoke(parameter);
            }
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


        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        /*   public void Execute2(object parameter)
           {
               try
               {
                   this.isExecuting = true;
                   this.Started?.Invoke(this, EventArgs.Empty);

                   Task task = Task.Factory.StartNew(() =>
                   {
                       //this.Execute(parameter);
                       execAction?.Invoke(parameter);
                   });
                   task.ContinueWith(t =>
                   {
                       this.OnRunWorkerCompleted(EventArgs.Empty);
                   }, TaskScheduler.FromCurrentSynchronizationContext());
               }
               catch (Exception ex)
               {
                   this.OnRunWorkerCompleted(new RunWorkerCompletedEventArgs(null, ex, true));
               }
           }*/

        public async Task ExecuteAsync(object parameter)
        {
            await _execAction(parameter);
        }

        /*   private void OnRunWorkerCompleted(EventArgs e)
           {
               this.isExecuting = false;
               this.Ended?.Invoke(this, e);
           }*/
    }
}