﻿using ScripTube.Models.Bookmark;
using ScripTube.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScripTube.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // https://stackoverflow.com/questions/1035023/firing-a-double-click-event-from-a-wpf-listview-item-using-mvvm
        private void xListViewBookmarkItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            var bookmarkItem = (BookmarkItem)listViewItem.Content;

            if (bookmarkItem != null)
            {
                xVideoPlayer.SeekTo(bookmarkItem.Seconds);
            }
        }

        private void xListViewBookmark_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (sender is ListBoxItem && e.LeftButton == MouseButtonState.Pressed)
            {
                ListBoxItem draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        private void xListViewBookmark_Drop(object sender, DragEventArgs e)
        {
            if (sender is ListViewItem)
            {
                var droppedData = e.Data.GetData(e.Data.GetFormats()[0]) as BookmarkItem;
                var target = ((ListViewItem)sender).DataContext as BookmarkItem;

                int removedIndex = xListViewBookmark.Items.IndexOf(droppedData);
                int targetIndex = xListViewBookmark.Items.IndexOf(target);

                var items = ((MainWindowViewModel)xListViewBookmark.DataContext).BookmarkItems;

                if (removedIndex < targetIndex)
                {
                    items.Insert(targetIndex + 1, droppedData);
                    items.RemoveAt(removedIndex);
                }
                else
                {
                    int remainIndex = removedIndex + 1;
                    if (items.Count + 1 > remainIndex)
                    {
                        items.Insert(targetIndex, droppedData);
                        items.RemoveAt(remainIndex);
                    }
                }

                ((MainWindowViewModel)xListViewBookmark.DataContext).TargetVideo.BookmarkTray.SaveAsJson();
            }
        }
    }
}