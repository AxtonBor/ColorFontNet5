using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfColorFont.Generico
{
    public interface ICommandAsync : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}