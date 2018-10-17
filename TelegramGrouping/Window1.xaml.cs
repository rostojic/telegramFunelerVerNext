/*
 * Created by SharpDevelop.
 * User: Sylwek
 * Date: 2015-04-24
 * Time: 20:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using DataGridGroupDemo.Models;
using DataGridGroupDemo.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace DataGridGroupDemo
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
    {
        ObservableCollection<Telegram> Telegrams;

        CancellationTokenSource cancelToken;

        Progress<double> progressOperation;
    

        public Window1()
		{
			InitializeComponent();
            Telegrams = new ObservableCollection<Telegram>();
        }


        async Task<ObservableCollection<Telegram>> LoadTelegramsAsync(CancellationToken ct, IProgress<double> progress)
        {
            Telegrams.Clear();
            
            var task = Task.Run(async () => {
                int recCount = 0;
                foreach (var file in TelegramsViewModel.fileNames)
                {
                    string[] allLInes = await GetAllLines(file);
                    long total = allLInes.GetLongLength(0);
                    foreach (var line in allLInes)
                    {
                        foreach (var pattern in TelegramConstants.AllPatterns)
                        {
                            if (Regex.IsMatch(line, pattern))
                            {
                                Telegrams.Add(new Telegram
                                {
                                    TelegramId = pattern.Substring(pattern.Length - 2),
                                    FileLine = line,
                                    FileName = file
                                });
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
                foreach (string filename in openFileDialog.FileNames)
                {
                    TelegramsViewModel.fileNames.Add(filename);
                }
                return  true;
            }
            else
            {
                return false;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            bool go =  DoLoadFiles();
            if (!go)
            {
                return;
            }
            
            cancelToken = new CancellationTokenSource();
            btnLoadFiles.IsEnabled = false;
            progressOperation = new Progress<double>(value => progress.Value = value);
            try
            {
                var Telgs = await LoadTelegramsAsync(cancelToken.Token, progressOperation);

                foreach (var item in Telgs)
                {
                    dgTel.Items.Add(item);
                }
            }
            catch (OperationCanceledException ex1)
            {

                throw;
            }
            catch (Exception ex2)
            {
                throw;
                //txtstatus.Text = "Operation cancelled" + ex.Message;
            }
            finally
            {
                cancelToken.Dispose();
                btnLoadFiles.IsEnabled = true;
                //btnCancel.IsEnabled = false;
            }
        }
	}
}