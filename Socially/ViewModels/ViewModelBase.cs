using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace Socially.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        private readonly Dispatcher _dispatcher;

        protected ViewModelBase()
        {
            _dispatcher = Application.Current != null ? Application.Current.Dispatcher : Dispatcher.CurrentDispatcher;
        }

        protected Dispatcher Dispatcher
        {
            get { return _dispatcher; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        #endregion

        ~ViewModelBase()
        {
            Dispose(false);
        }
    }
}