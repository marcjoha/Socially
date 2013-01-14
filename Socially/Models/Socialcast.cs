using System.ComponentModel;
using ScApi;
using ScApi.Data;

// denna klassen skall exponera datatyperna från scapi fast wrappa dom i propertychanged
// viewmodel sköter ev. omskrivning av innehållet

namespace Socially.Models
{
    public class Socialcast : INotifyPropertyChanged
    {
        private readonly SocialcastApi _scapi;
        private Community _community;

        public Socialcast(string communityUrl, string email, string password)
        {
            _scapi = new SocialcastApi(communityUrl, email, password);
            _community = _scapi.GetCommunity();
        }

        public Community Community
        {
            get { return _community; }
            set
            {
                if (_community != value)
                {
                    _community = value;
                    RaisePropertyChanged("Community");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}