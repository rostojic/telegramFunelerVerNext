
using System;
using DataGridGroupDemo.Models;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.ObjectModel;
using DataGridGroupDemo.Commands;

namespace DataGridGroupDemo.ViewModels
{
    /// <summary>
    /// Description of TelegramsViewModel.
    /// </summary>
    public class TelegramsViewModel
	{
		public ICollectionView TelegramsView {set; get;}
        public static string[] fileNames = new string[]
        {   @"C:\Projects\autofmc\FMC\MFC\bin\LOG\FMC_APAL-20181016-00001.log" ,
            @"C:\Projects\manualfmc\MFC\bin\LOG\FMC_MPAL-20181011-00001.log"};
        public TelegramsViewModel()
		{
            
			IList<Telegram> telegrams = new Telegrams(fileNames);
            TelegramsView = CollectionViewSource.GetDefaultView(telegrams);
			TelegramsView.GroupDescriptions.Add(new PropertyGroupDescription("noGroup"));

            groupByTelegramCommand = new GroupByTelegramCommand(this);
			groupByFileNameCommand = new GroupByFileNameCommand(this);
			removeGroupCommand = new RemoveGroupCommand(this);
		}
		
		public void RemoveGroup()
		{
            TelegramsView.GroupDescriptions.Clear();
            //TelegramsView.GroupDescriptions.Add(new PropertyGroupDescription("noGroup"));
		}

		public void GroupByTelegram()
		{
			TelegramsView.GroupDescriptions.Clear();
            TelegramsView.GroupDescriptions.Add(new PropertyGroupDescription("TelegramId"));
		}

		public void GroupByFileName()
		{
            TelegramsView.GroupDescriptions.Clear();
            TelegramsView.GroupDescriptions.Add(new PropertyGroupDescription("FileName"));
		}
		
		public ICommand groupByTelegramCommand {
			get;
			private set;
		}
		
		public ICommand groupByFileNameCommand {
			get;
			private set;
		}
		
		public ICommand removeGroupCommand {
			get;
			private set;
		}
	}
	
	
	public class GroupsToTotalConverter: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is ReadOnlyObservableCollection<object>) {
				var items = (ReadOnlyObservableCollection<object>)value;
				Decimal total = 0;
				foreach (Telegram element in items) {
					total += 1;
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
