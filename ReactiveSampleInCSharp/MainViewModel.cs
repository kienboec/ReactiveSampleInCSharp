using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReactiveSampleInCSharp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _filterString;
        private IReadOnlyList<string> _data;

        public string FilterString
        {
            get
            {
                return _filterString;
            }
            set
            {
                if (_filterString != value)
                {
                    _filterString = value;
                    OnPropertyChanged();
                }
            }
        }

        public IReadOnlyList<string> Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            this.Data = DishDataSource.Dishes;
            
            // nuget: System.Reactive
            // changes in data -> value sequences in time
            // observers can subscribe to changes
            // LINQ 
            Observable
                .FromEventPattern(this, "PropertyChanged")
                .Where(x => ((PropertyChangedEventArgs)x.EventArgs).PropertyName == "FilterString")
                .Select(x => FilterString.ToUpper())
                .Where(x => x.Length > 3)
                .Throttle(TimeSpan.FromMilliseconds(5000))
                .DistinctUntilChanged()
                // .ObserveOn(SynchronizationContext.Current)
                .Subscribe(filterString =>
                {
                    this.Data = DishDataSource.Dishes
                        .Where(x => x.ToUpper().Contains(filterString))
                        .ToList()
                        .AsReadOnly();
                });

            // Observable.Start(() => 123); // 1 item then end
            // Task.CompletedTask.ToObservable() // wait then result then end
            // IEnumerable .ToObservable() // items then end
            // Observable.FromEventPattern
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
