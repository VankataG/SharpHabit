using System.Windows.Input;

namespace SharpHabit.Wpf.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T?> execute;

        private readonly Func<T?, bool>? canExecute;

        public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (canExecute == null) return true;
            return canExecute((T?)parameter);
        }

        public void Execute(object? parameter) => execute((T?)parameter);


        public event EventHandler? CanExecuteChanged;

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
