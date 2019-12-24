using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using TelegramGrid.Models;
using TelegramGrid.ViewModels;

namespace TelegramGrid
{
    /// <summary>
    /// Interaction logic for MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        // ObservableCollection<Telegram> Telegrams;

        public ICollectionView cvTelegrams;
        MainViewModel _vm = new MainViewModel();

        public MainForm()
		{
			InitializeComponent();
            DataContext = _vm;
        }

        private bool  DoLoadFiles()
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Log files (*.log)|*.log|All files (*.*)|*.*",
            };

            if (openFileDialog.ShowDialog() == true)
            {
                HosueKeeping();
                foreach (string filename in openFileDialog.FileNames)
                {
                    LoadedFileNames.fileNames.Add(filename);
                }
                return  true;
            }
            else
            {
                return false;
            }
        }

        private void HosueKeeping()
        {
            _vm.Telegrams.Clear();
            LoadedFileNames.fileNames.Clear();
        }

        private void Button_Load_Files_Click(object sender, RoutedEventArgs e)
        {
            bool go =  DoLoadFiles();
            if (!go)
            {
                return;
            }
            
            btnLoadFiles.IsEnabled = false;
            try
            {
                _vm.StartLoadingAllTelegrams();
                //_vm.LoadAllTelegrams();
             }
            catch (OperationCanceledException ex1)
            {
                throw new Exception(ex1.Message);
            }
            catch (Exception ex2)
            {
                throw new Exception(ex2.Message); 
            }
            finally
            {
                btnLoadFiles.IsEnabled = true;
            }
        }

        private bool TextFilter(object o)
        {
            bool rv = false;
            Telegram t = (o as Telegram);
            if (t == null)
                return rv;

            switch (cboFilterChoice.SelectedIndex)
            {
                //telegram id
                case 0:
                    if (t.TelegramId == txtFilterValue.Text)
                        rv = true;
                    else
                        rv = false;
                    break;
                //location prefix
                case 1:
                    if (t.LocationPrefix == txtFilterValue.Text)
                        rv = true;
                    else
                        rv = false;
                    break;
                //location date time
                case 2:
                    if (t.DateAndTime.StartsWith(txtFilterValue.Text))
                        rv = true;
                    else
                        rv = false;
                    break;
                //tsu id or whatever ...
                case 3:
                    if (t.TelegramContent.Contains(txtFilterValue.Text))
                        rv = true;
                    else
                        rv = false;
                    break;
                default:
                    break;

            }
            return rv;
        }


        private void btnFilterClear_Click(object sender, RoutedEventArgs e)
        {
            if(cvTelegrams != null)
            {
                cvTelegrams.Filter = null;
            }
            txtFilterValue.Text = string.Empty;
        }

        private void SetCollectionViewSource()
        {
            cvTelegrams = CollectionViewSource.GetDefaultView(_vm.Telegrams);
            dgTel.ItemsSource = cvTelegrams;
        }

        private void btnSetFilter_Click(object sender, RoutedEventArgs e)
        {
            if (cvTelegrams == null)
            {
                SetCollectionViewSource();
            }
            cvTelegrams.Filter = TextFilter;
        }
    }
}