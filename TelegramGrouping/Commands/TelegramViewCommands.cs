using System;
using System.Windows.Input;
using DataGridGroupDemo.ViewModels;

namespace DataGridGroupDemo.Commands
{
	public class RemoveGroupCommand: ICommand
	{
		#region ICommand implementation

	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.RemoveGroup();
	}

	#endregion

		private TelegramsViewModel _viewModel;
		
		public RemoveGroupCommand(TelegramsViewModel viewModel)
		{
			this._viewModel = viewModel;
		}
	}

	public class GroupByTelegramCommand: ICommand
	{
		#region ICommand implementation

	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByTelegram();
	}

	#endregion

		private TelegramsViewModel _viewModel;
		
		public GroupByTelegramCommand(TelegramsViewModel viewModel)
		{
			this._viewModel = viewModel;
		}
	}

	
	public class GroupByFileNameCommand: ICommand
	{
		#region ICommand implementation

	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object parameter)
	{
		return true;
	}

	public void Execute(object parameter)
	{
		this._viewModel.GroupByFileName();
	}

	#endregion

		private TelegramsViewModel _viewModel;
		
		public GroupByFileNameCommand(TelegramsViewModel viewModel)
		{
			this._viewModel = viewModel;
		}
	}
	
}
