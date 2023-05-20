using System;
using System.Windows.Input;

namespace MultiHeaderSample
{
    class DelegateCommand : ICommand
    {
        private readonly Action executeMethod;

        private readonly Func<bool> canExecuteMethod;

        public DelegateCommand(Action executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecuteMethod != null ? this.canExecuteMethod() : true;
        }

        public void Execute(object parameter)
        {
            if (this.executeMethod != null)
            {
                this.executeMethod();
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }

    public class DelegateCommands : ICommand
    {
        private readonly Action<object> executeMethod;

        private readonly Func<bool> canExecuteMethod;

        public DelegateCommands(Action<object> executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommands(Action<object> executeMethod, Func<bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecuteMethod != null ? this.canExecuteMethod() : true;
        }

        public void Execute(object parameter)
        {
            if (this.executeMethod != null)
            {
                this.executeMethod(parameter);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
