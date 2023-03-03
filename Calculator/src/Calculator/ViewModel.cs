using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<String> _history;
        public MyViewModel()
        {
            _history = new ObservableCollection<String>();
        }

        public ObservableCollection<String> history
        {
            get => _history;
        }

        internal void addToHistory(String expr)
        {
            _history.Add(expr);
            OnPropertyChanged("history");
            OnPropertyChanged();
        }
        public void OnPropertyChanged([CallerMemberName] string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    }
