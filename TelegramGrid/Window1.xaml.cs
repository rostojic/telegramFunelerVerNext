using DataGridGroupDemo.Models;
using DataGridGroupDemo.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DataGridGroupDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        ObservableCollection<Telegram> Telegrams;
        ICollectionView cvTelegrams;

        CancellationTokenSource cancelToken;

        Progress<double> progressOperation;
        Progress<string> progressFileName;

        public object Deployment { get; private set; }

        public Window1()
		{
			InitializeComponent();
            Telegrams = new ObservableCollection<Telegram>();
        }


        async Task<ObservableCollection<Telegram>> LoadTelegramsAsync(CancellationToken ct, IProgress<double> progress, IProgress<string> fileName)
        {
            Telegrams.Clear();
            var task = Task.Run(async () => {
                int recCount = 0;
                StringBuilder allFilesLoaded = new StringBuilder();
                foreach (var file in LoadedFileNames.fileNames)
                {
                    allFilesLoaded.Append(file);
                    allFilesLoaded.Append("; ");
                    fileName.Report(allFilesLoaded.ToString());
                    string[] allLInes = await GetAllLines(file);
                    long total = allLInes.GetLongLength(0);
                    foreach (var line in allLInes)
                    {
                        foreach (var pattern in TelegramConstants.AllPatterns)
                        {
                            if (Regex.IsMatch(line, pattern))
                            {
                                bool isStartedWithP = Regex.Matches(line, "\"([^\"]*)\"")[0].ToString().StartsWith("\"P");
                                if (!isStartedWithP)
                                {
                                    string tc = Regex.Matches(line, "\"([^\"]*)\"")[0].ToString();
                                    string tlgId = pattern.Substring(pattern.Length - 2);
                                    int tlgLocPrefStart = 0;
                                    TelegramConstants.TelegramLocationPrefixes.TryGetValue(tlgId, out tlgLocPrefStart);
                                    var tlg = new Telegram
                                    {
                                        TelegramId = tlgId,
                                        DateAndTime = line.Substring(0, 23),
                                        TelegramContent = tc,
                                        FileName = file,
                                        LocationPrefix = tlgLocPrefStart == 0 ? "NOLOC" : tc.Substring(tlgLocPrefStart, 4)
                                    };
                                    Telegrams.Add(tlg);
                                }
                            }
                        }
                        ++recCount;
                        progress.Report(recCount * 100.0 / total);
                    }
                    recCount = 0;
                    progress.Report(recCount);
                }

                return Telegrams;

            });

            return await task;
        }

     
        async Task<string[]> GetAllLines(string fileName)
        {
            using (var reader = File.OpenText(fileName))
            {
                string fileText = await reader.ReadToEndAsync();
                string[] allLInes = fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                return allLInes;
            }
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
            Telegrams.Clear();
            LoadedFileNames.fileNames.Clear();
            dgTel.ItemsSource = null;
        }

        private async void Button_Load_Files_Click(object sender, RoutedEventArgs e)
        {
            bool go =  DoLoadFiles();
            if (!go)
            {
                return;
            }
            
            cancelToken = new CancellationTokenSource();
            btnLoadFiles.IsEnabled = false;
            progressOperation = new Progress<double>(value => progress.Value = value);
            progressFileName = new Progress<string>(value => txtFileName.Text = value);
            try
            {
                var Telgs = await LoadTelegramsAsync(cancelToken.Token, progressOperation, progressFileName);
                cvTelegrams = CollectionViewSource.GetDefaultView(Telegrams);
                dgTel.ItemsSource = cvTelegrams;
               }
            catch (OperationCanceledException ex1)
            {

                throw;
            }
            catch (Exception ex2)
            {
                throw;
            }
            finally
            {
                cancelToken.Dispose();
                btnLoadFiles.IsEnabled = true;
            }
        }

        private bool TextFilter(object o)
        {
            Telegram t = (o as Telegram);
            if (t == null)
                return false;
            
            if(cboFilterChoice.SelectedIndex == 0)
            {
                if (t.TelegramId == txtFilterValue.Text)
                    return true;
                else
                    return false;
            }
            else if (cboFilterChoice.SelectedIndex == 1)
            {
                if (t.LocationPrefix == txtFilterValue.Text)
                    return true;
                else
                    return false;
            }
            else if (cboFilterChoice.SelectedIndex == 2)
            {
                if (t.DateAndTime.StartsWith(txtFilterValue.Text))
                    return true;
                else
                    return false;
            }
            else 
            {
                if (t.FileName == txtFilterValue.Text)
                    return true;
                else
                    return false;
            }
        }


        private void btnFilterClear_Click(object sender, RoutedEventArgs e)
        {
            if(cvTelegrams != null)
            {
                cvTelegrams.Filter = null;
                txtFilterValue.Text = string.Empty;
            }
        }

        private void btnSetFilter_Click(object sender, RoutedEventArgs e)
        {
            if (cvTelegrams != null)
            {
                cvTelegrams.Filter = TextFilter;
            }
        }
    }
}