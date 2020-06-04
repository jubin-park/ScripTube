﻿using ScripTube.Models.YouTube;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ScripTube.ViewModels.Commands
{
    class ExecutePapagoCommand : ICommand
    {
        private static string NEW_LINE = "%0A%0A";
        private static string PAPAGO_URL = "https://papago.naver.com/?sk=auto&tk=ko&hn=1&st=";
        private static int MAX_STRING_SIZE = 5000;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public MainWindowViewModel ViewModel { get; }

        public ExecutePapagoCommand(MainWindowViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            IEnumerable enumerable = parameter as IEnumerable;
            if (enumerable != null)
            {
                var subtitleItems = enumerable.OfType<SubtitleItem>().OrderBy(s => s.StartSeconds).ToList();
                return subtitleItems.Count > 0;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            IEnumerable enumerable = parameter as IEnumerable;
            if (enumerable != null)
            {
                var subtitleItems = enumerable.OfType<SubtitleItem>().OrderBy(s => s.StartSeconds).ToList();
                var sb = new StringBuilder(PAPAGO_URL);
                int count = 0;
                foreach (var item in subtitleItems)
                {
                    if (count + item.Text.Length > MAX_STRING_SIZE)
                    {
                        System.Diagnostics.Process.Start(sb.ToString());
                        count = 0;
                        sb.Clear();
                        sb.Append(PAPAGO_URL);
                    }
                    else
                    {
                        sb.Append(item.Text);
                        sb.Append(NEW_LINE);
                        count += item.Text.Length + NEW_LINE.Length;
                    }
                }
                System.Diagnostics.Process.Start(sb.ToString());
            }
        }
    }
}