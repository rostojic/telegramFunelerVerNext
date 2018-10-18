using System.ComponentModel;

namespace TelegramGrid.Models
{
    public class Telegram 
    {
        public string TelegramId { get; set; }
		public string DateAndTime { get; set; }
        public string TelegramContent { get; set; }
        public string LocationPrefix { get; set; }
        public string FileName { get; set; }
    }
}
