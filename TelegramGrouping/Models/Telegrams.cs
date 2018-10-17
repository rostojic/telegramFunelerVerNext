using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataGridGroupDemo.Models
{
	/// <summary>
	/// Description of Oredr.
	/// </summary>
	public class Telegram
	{
		public string TelegramId	{set; get;}
		public string FileLine		{set; get;}
		public string FileName		{set; get;}
	
			
		public string noGroup {
			get {
				return "Total";
			}
		}
	}
	
	public class Telegrams: ObservableCollection<Telegram>
	{
		public Telegrams(string[] fileNames)
		{
            foreach (var file in fileNames)
            {
                string[] allLInes = File.ReadAllLines(file);
                foreach (var line in allLInes)
                {
                    foreach (var pattern in TelegramConstants.AllPatterns)
                    {
                        if (Regex.IsMatch(line, pattern))
                        {
                            this.Add(new Telegram
                            {
                                TelegramId = pattern.Substring(pattern.Length - 2),
                                FileLine = line,
                                FileName = file
                            });
                        }
                    }
                }
            }
        }
    }
}
