using System.Collections.Generic;

namespace TelegramGrid.Models
{
    public static class TelegramConstants
    {
        public static Dictionary<string, int> TelegramLocationPrefixes = 
            new Dictionary<string, int>
        {
            {"01", 35 },
            {"02", 35 },
            {"06", 35 },
            {"07", 35 },
            {"08", 35 },
            {"11", 35 },
            {"12", 35 },
            {"16", 35 },
            {"18", 35 },
            {"51", 35 },
            {"52", 35 },
            {"56", 35 },
            {"57", 35 },
            {"58", 35 },
            {"65", 35 },
            {"71", 35 },
            {"72", 35 },
            {"73", 35 },
            {"83", 35 },
            {"90", 15 }
        };
   
        public static string[] AllPatterns =    
            new string[] {
            @"\].01",
            @"\].02",
            @"\].06",
            @"\].07",
            @"\].08",
            @"\].11",
            @"\].12",
            @"\].16",
            @"\].18",
            @"\].51",
            @"\].52",
            @"\].56",
            @"\].57",
            @"\].58",
            @"\].65",
            @"\].71",
            @"\].72",
            @"\].73",
            @"\].83",
            @"\].90"
        };
    }
}