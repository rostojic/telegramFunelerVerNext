﻿namespace DataGridGroupDemo.Models
{
    /// <summary>
    /// Description of Oredr.
    /// </summary>
    public class Telegram
	{
		public string TelegramId	{set; get;}
		public string DateAndTime	{set; get;}
        public string TelegramContent { set; get; }
        public string LocationPrefix { set; get; }
        public string FileName		{set; get;}
	}
}