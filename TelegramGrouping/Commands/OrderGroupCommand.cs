/*
 * Created by SharpDevelop.
 * User: Sylwek
 * Date: 2015-04-20
 * Time: 20:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Input;
using DataGridGroupDemo.ViewModel;

namespace DataGridGroupDemo.Commands
{
	/// <summary>
	/// Description of OrderGroupCommand.
	/// </summary>
	public class RemoveGroupCommand: ICommand
	{
		#region ICommand implementation

		public event EventHandler CanExecuteChanged;
	
		public bool CanExecute(object parameter)
		{
			return true;
			//throw new NotImplementedException();
		}
	
		public void Execute(object parameter)
		{
			//throw new NotImplementedException();
			_viewModel.RemoveGroup();
		}
	
		#endregion

		public RemoveGroupCommand(OrdersViewModel viewModel)
		{
			_viewModel = viewModel;
		}
		
		private OrdersViewModel _viewModel;
	}
	
	public class GroupByCustomerCommand: ICommand
	{
		#region ICommand implementation

		public event EventHandler CanExecuteChanged;
	
		public bool CanExecute(object parameter)
		{
			return true;
			//throw new NotImplementedException();
		}
	
		public void Execute(object parameter)
		{
			//throw new NotImplementedException();
			_viewModel.GroupByCustomer();
		}
	
		#endregion

		public GroupByCustomerCommand(OrdersViewModel viewModel)
		{
			_viewModel = viewModel;
		}
		
		private OrdersViewModel _viewModel;
	}

	public class GroupByYearMonthCommand: ICommand
	{
		#region ICommand implementation

		public event EventHandler CanExecuteChanged;
	
		public bool CanExecute(object parameter)
		{
			return true;
			//throw new NotImplementedException();
		}
	
		public void Execute(object parameter)
		{
			//throw new NotImplementedException();
			_viewModel.GroupByYearMonth();
		}
	
		#endregion

		public GroupByYearMonthCommand(OrdersViewModel viewModel)
		{
			_viewModel = viewModel;
		}
		
		private OrdersViewModel _viewModel;
	}


}
