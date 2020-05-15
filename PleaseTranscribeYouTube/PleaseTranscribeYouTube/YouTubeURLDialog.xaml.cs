﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PleaseTranscribeYouTube
{
    /// <summary>
    /// YouTubeURLDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class YouTubeURLDialog : UserControl
    {
        public string VideoIDOrNull;

        public YouTubeURLDialog()
        {
            InitializeComponent();
            xTextBoxURL.Text = Clipboard.GetText();
        }

        private void xButtonOK_Click(object sender, RoutedEventArgs e)
        {
            string url = xTextBoxURL.Text;
            string idOrNull = getYouTubeVideoIDOrNull(url);
            if (idOrNull == null || idOrNull.Length <= 1)
            {
                VideoIDOrNull = null;
            }
            else
            {
                VideoIDOrNull = idOrNull;
                xButtonClose.Command.Execute(true); // xButtonClose.CommandParameter
            }
        }

        private string getYouTubeVideoIDOrNull(string url)
        {
            try
            {
                Uri uri = new Uri(url);
                string host = uri.Host;
                if (host != "www.youtube.com" && host != "youtu.be")
                {
                    return null;
                }
                NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);
                if (query.AllKeys.Contains("v"))
                {
                    return query["v"];
                }
                else if (query.AllKeys.Contains("video_id"))
                {
                    return query["video_id"];
                }
                return null;
            }
            catch (System.UriFormatException)
            {
                return null;
            }
        }

        private void xTextBoxURL_TextChanged(object sender, TextChangedEventArgs e)
        {
            string idOrNull = getYouTubeVideoIDOrNull(xTextBoxURL.Text);
            if (idOrNull != null && idOrNull.Length > 0)
            {
                xTextBlockError.Visibility = Visibility.Hidden;
            }
            else
            {
                xTextBlockError.Visibility = Visibility.Visible;
            }
        }
    }
}
