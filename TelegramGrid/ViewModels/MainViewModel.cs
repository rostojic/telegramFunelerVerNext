using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;
using TelegramGrid.Models;

namespace TelegramGrid.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Telegram> Telegrams { get; set; }

        private object _telegramLock = new object();

        private bool viewCounter = true;

        public bool ViewCounter
        {
            get { return viewCounter; } set { viewCounter = value; NotifyPropertyChanged("ViewCounter"); }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            Telegrams = new ObservableCollection<Telegram>();
            
            BindingOperations.EnableCollectionSynchronization(Telegrams, _telegramLock);
        }

        string[] GetAllLines(string fileName)
        {
            using (var reader = File.OpenText(fileName))
            {
                string fileText =  reader.ReadToEnd();
                string[] allLInes = fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                return allLInes;
            }
        }

        public void StartLoadingAllTelegrams()
        {
            Task.Factory.StartNew(() =>
            {
                LoadAllTelegrams();
            });
        }

        public void LoadAllTelegrams()
        {
            ViewCounter = true;
            StringBuilder allFilesLoaded = new StringBuilder();
            foreach (var file in LoadedFileNames.fileNames)
            {
                string[] allLInes =  GetAllLines(file);
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
                }
            }
            ViewCounter = false;
        }
    }
}
