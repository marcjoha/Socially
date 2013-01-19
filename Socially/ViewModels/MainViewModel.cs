using System.Collections.ObjectModel;
using ScApi.Data;

namespace Socially.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly Community _community;
        private ObservableCollection<MessageViewModel> _homeStream;
        private ObservableCollection<MessageViewModel> _mentionsStream;
        private ObservableCollection<MessageViewModel> _sentStream;
        private ObservableCollection<MessageViewModel> _companyStream;

        public MainViewModel(Community community)
        {
            _community = community;
        }

        public string WindowTitle
        {
            get { return string.Format("Socially - {0}", _community.Name); }
        }

        public string ProfilePhotoUrl
        {
            get { return _community.Profile.Avatars.Square140; }
        }

        public string ProfilePhotoToolTip
        {
            get { return string.Format("{0}, {1}", _community.Profile.Name, _community.Profile.Title); }
        }

        public ObservableCollection<MessageViewModel> HomeStream
        {
            get { return _homeStream; }
            set
            {
                if (_homeStream == value) return;
                _homeStream = value;
                OnPropertyChanged("HomeStream");
            }
        }

        public ObservableCollection<MessageViewModel> MentionsStream
        {
            get { return _mentionsStream; }
            set
            {
                if (_mentionsStream == value) return;
                _mentionsStream = value;
                OnPropertyChanged("MentionsStream");
            }
        }

        public ObservableCollection<MessageViewModel> SentStream
        {
            get { return _sentStream; }
            set
            {
                if (_sentStream == value) return;
                _sentStream = value;
                OnPropertyChanged("SentStream");
            }
        }

        public ObservableCollection<MessageViewModel> CompanyStream
        {
            get { return _companyStream; }
            set
            {
                if (_companyStream == value) return;
                _companyStream = value;
                OnPropertyChanged("CompanyStream");
            }
        }
    }
}