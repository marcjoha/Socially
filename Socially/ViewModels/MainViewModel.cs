using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ScApi;
using ScApi.Data;

namespace Socially.ViewModels
{
    public class MainViewModel
    {
        private readonly SocialcastApi _scapi;
        private readonly Community _community;

        public MainViewModel()
        {
            _scapi = new SocialcastApi("https://demo.socialcast.com", "emily@socialcast.com", "demo");
            _community = _scapi.GetCommunity();
        }

        public string WindowTitle
        {
            // Concatenate string for window title
            get { return string.Format("Socially - {0}", _community.Name); }
        }

        public string ProfilePhotoUrl
        {
            get { return _community.Profile.Avatars.Square140; }
        }

        public string ProfilePhotoToolTip
        {
            // Concatenate name and title to use as image tooltip
            get { return string.Format("{0}, {1}", _community.Profile.Name, _community.Profile.Title); }
        }

        public ObservableCollection<MessageViewModel> HomeStream
        {
            get { return new ObservableCollection<MessageViewModel>(GetStream("api/messages")); }
        }

        private List<MessageViewModel> GetStream(string resource)
        {
            return _scapi.GetStream(resource).Select(message => new MessageViewModel(message)).ToList();
        }
    }
}