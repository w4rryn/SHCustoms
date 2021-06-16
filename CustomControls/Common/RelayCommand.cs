using System;
using System.Windows.Input;

namespace CustomControls.Common
{
	internal class RelayCommand : ICommand
	{
		private readonly Func<object, bool> canExecute;

		private readonly Action<object> execute;

		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
		{
			this.execute = execute;
			this.canExecute = canExecute;
		}

		public RelayCommand(Action<object> execute)
			: this(execute, (object o) => true)
		{
		}

		public bool CanExecute(object parameter)
		{
			return (canExecute?.Invoke(parameter)).Value;
		}

		public void Execute(object parameter)
		{
			execute?.Invoke(parameter);
		}
	}
}
