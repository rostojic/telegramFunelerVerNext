﻿using System.Collections.Generic;

namespace DataGridGroupDemo.Models
{
    public static class TelegramConstants
    {

        
        public static Dictionary<string, int> TelegramLocationPrefixes = 
            new Dictionary<string, int>
        {
            {"01", 35 },
            {"02", 35 },
            {"03", 35 },
            {"04", 35 },
            {"07", 35 },
            {"08", 35 },
            {"11", 35 },
            {"12", 35 },
            {"14", 35 },
            {"18", 35 },
            {"31", 15 },
            {"32", 15 },
            {"33", 15 },
            {"34", 15 },
            {"35", 15 },
            {"40", 15 },
            {"43", 35 },
            {"44", 35 },
            {"47", 35 },
            {"51", 35 },
            {"52", 35 },
            {"54", 35 },
            {"57", 35 },
            {"72", 15 },
            {"73", 35 },
            {"82", 15 }
        };
   
 
        public static string[] AllPatterns =    new string[] { @"\].01",
                                                            @"\].02",
                                                            @"\].03",
                                                            @"\].04",
                                                            @"\].07",
                                                            @"\].08",
                                                            @"\].11",
                                                            @"\].12",
                                                            @"\].14",
                                                            @"\].18",
                                                            @"\].30",
                                                            @"\].31",
                                                            @"\].32",
                                                            @"\].33",
                                                            @"\].34",
                                                            @"\].35",
                                                            @"\].36",
                                                            @"\].37",
                                                            @"\].38",
                                                            @"\].39",
                                                            @"\].40",
                                                            @"\].41",
                                                            @"\].43",
                                                            @"\].44",
                                                            @"\].45",
                                                            @"\].46",
                                                            @"\].47",
                                                            @"\].51",
                                                            @"\].52",
                                                            @"\].54",
                                                            @"\].57",
                                                            @"\].58",
                                                            @"\].60",
                                                            @"\].61",
                                                            @"\].65",
                                                            @"\].72",
                                                            @"\].73",
                                                            @"\].80",
                                                            @"\].81",
                                                            @"\].82",
                                                            @"\].83",
                                                            @"\].90",
                                                            @"\].92",
                                                            @"\].98",
                                                            @"\].99"
        };

    }
}