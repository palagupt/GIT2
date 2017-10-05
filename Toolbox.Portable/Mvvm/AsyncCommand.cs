using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Toolbox.MVVM.Input
{
    public class AsyncCommand : ICommand
    {
        Func<object, Task> action;
        Task task;

        public AsyncCommand(Func<object, Task> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return task == null || task.IsCompleted;
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        private void OnCanExecuteChanged()
        {
            var handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public async Task ExecuteAsync(object parameter)
        {
            task = action(parameter);

            OnCanExecuteChanged();

            await task;

            OnCanExecuteChanged();
        }
    }
}
