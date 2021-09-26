using System.Threading.Tasks;
using System.Windows.Input;

namespace Generico
{
    public interface ICommandAsync : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}