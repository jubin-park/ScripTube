﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScripTube.Classes.YouTube
{
    public class Subtitle
    {
        public string LanguageCode { get; }
        public string LanguageName { get; }
        public bool IsAutoGenerated { get; } // automatic-speech-recognition

        private ObservableCollection<SubtitleItem> mItems = new ObservableCollection<SubtitleItem>();
        public ObservableCollection<SubtitleItem> Items
        {
            get { return mItems; }
        }

        public Subtitle(JToken languageCode, JToken languageName, JToken asr)
        {
            LanguageCode = languageCode.ToString();
            LanguageName = languageName.ToString();
            IsAutoGenerated = (asr != null && asr.ToString() == "asr");
        }

        public void AddItem(SubtitleItem item)
        {
            mItems.Add(item);
        }
    }
}