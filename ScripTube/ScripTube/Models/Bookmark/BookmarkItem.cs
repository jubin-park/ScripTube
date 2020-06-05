﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ScripTube.Models.Bookmark
{
    public class BookmarkItem
    {
        public double Seconds { get; set; }
        public string Memo { get; set; }
        public string ImagePath { get; set; }
        public BitmapImage BitmapImage { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
