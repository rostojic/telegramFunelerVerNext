/*
 * Created by SharpDevelop.
 * User: Sylwek
 * Date: 2015-04-20
 * Time: 20:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using DataGridGroupDemo.Models;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using DataGridGroupDemo.Commands;

using System.Collections.ObjectModel;

namespace DataGridGroupDemo.ViewModel
{
	
	
	/// <summary>
	/// Description of OrdersViewModel.
	/// </summary>
	public class OrdersViewModel
	{
		public OrdersViewModel()
		{
			IList<Order> orders = new Orders();
			ordersView = CollectionViewSource.GetDefaultView(orders);
			ordersView.GroupDescriptions.Add(new PropertyGroupDescription("noGroup"));
			
			groupByCustomerCommand = new GroupByCustomerCommand(this);
			removeGroupCommand = new RemoveGroupCommand(this);
			groupByYearMonthCommand = new GroupByYearMonthCommand(this);
		}
		
		
		public ICollectionView ordersView {set; get;}
		
		
		public void RemoveGroup()
		{
			ordersView.GroupDescriptions.Clear();
			ordersView.GroupDescriptions.Add(new PropertyGroupDescription("noGroup"));	
		}
		
		public void GroupByCustomer()
		{
			ordersView.GroupDescriptions.Clear();
			ordersView.GroupDescriptions.Add(new PropertyGroupDescription("customer"));	
		}
		
		public void GroupByYearMonth()
		{
			ordersView.GroupDescriptions.Clear();
			ordersView.GroupDescriptions.Add(new PropertyGroupDescription("orderYearMonth"));	
		}
		
		public ICommand groupByCustomerCommand {
			get;
			private set;
		}


		public ICommand removeGroupCommand {
			get;
			private set;
		}

		public ICommand groupByYearMonthCommand {
			get;
			private set;
		}

	}
	
	
	public class GroupsToTotalConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is ReadOnlyObservableCollection<object>) {
				var items = (ReadOnlyObservableCollection<object>)value;
				Decimal total = 0;
				foreach (Order gi in items) {
					total += gi.amount;
				}
				return total.ToString();
			}
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value;
		}
	} 
	
}
