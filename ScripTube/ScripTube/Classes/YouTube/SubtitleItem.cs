﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScripTube.Classes.YouTube
{
    public class SubtitleItem
    {
        public string Text { get; }
        public double StartSeconds { get;}
        public double DurationSeconds { get; }
        public bool IsOneHourExcessed { get; }

        public string StartTime
        {
            get
            {
                int total = Convert.ToInt32(StartSeconds);
                int min = total / 60;
                int sec = total % 60;
                if (IsOneHourExcessed)
                {
                    int hour = min / 60;
                    min %= 60;
                    return $"{hour.ToString("D2")}:{min.ToString("D2")}:{sec.ToString("D2")}";
                }
                return $"{min.ToString("D2")}:{sec.ToString("D2")}";
            }
        }

        public SubtitleItem(string text, string start, string duration, bool bOneHourExcessed)
        {
            Text = unescapeXml(text);
            StartSeconds = double.Parse(start);
            DurationSeconds = double.Parse(duration);
            IsOneHourExcessed = bOneHourExcessed;
        }

        private string unescapeXml(string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace("&#34;", "\"");
            sb.Replace("&#38;", "&");
            sb.Replace("&#39;", "'");
            sb.Replace("&#60;", "<");
            sb.Replace("&#62;", ">");
            return sb.ToString();
        }
    }
}