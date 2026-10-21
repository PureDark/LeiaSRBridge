using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Odyssey3DHub
{
	// Token: 0x02000005 RID: 5
	[NullableContext(2)]
	[Nullable(0)]
	public class RelayCommand : ICommand
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002325 File Offset: 0x00000525
		public RelayCommand([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}
			this._execute = execute;
			this._canExecute = canExecute;
			base..ctor();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000234A File Offset: 0x0000054A
		public RelayCommand([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> execute) : this(execute, null)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002354 File Offset: 0x00000554
		public bool CanExecute(object parameter)
		{
			Predicate<object> canExecute = this._canExecute;
			return canExecute == null || canExecute.Invoke(parameter);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002368 File Offset: 0x00000568
		public void Execute(object parameter)
		{
			this._execute.Invoke(parameter);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600001D RID: 29 RVA: 0x00002376 File Offset: 0x00000576
		// (remove) Token: 0x0600001E RID: 30 RVA: 0x0000237E File Offset: 0x0000057E
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

		// Token: 0x04000005 RID: 5
		[Nullable(new byte[]
		{
			1,
			2
		})]
		private readonly Action<object> _execute;

		// Token: 0x04000006 RID: 6
		private readonly Predicate<object> _canExecute;
	}
}
