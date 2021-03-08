using System;
using System.Windows.Input;

using NexusLabs.Contracts;

namespace Assets.Scripts.Gui.Noesis
{
    public class DelegateCommand : ICommand
    {
        private Func<object, bool> _canExecute;
        private Action<object> _execute;

        public DelegateCommand(Action<object> execute)
            : this(_ => true, execute)
        {
        }

        public DelegateCommand(Func<object, bool> canExecute, Action<object> execute)
        {
            Contract.RequiresNotNull(canExecute, nameof(canExecute));
            Contract.RequiresNotNull(execute, nameof(execute));
            _canExecute = canExecute;
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}