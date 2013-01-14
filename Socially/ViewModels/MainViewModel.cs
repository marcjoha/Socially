using ScApi.Data;
using Socially.Models;

namespace Socially.ViewModels
{
    public class MainViewModel
    {
        private string _windowTitle;
        private Socialcast _socialcast;
        private Community _community;
        private string _profilePhotoToolTip;

        public MainViewModel()
        {
            _socialcast = new Socialcast("https://demo.socialcast.com", "emily@socialcast.com", "demo");

            _community = _socialcast.Community;

            // Concatenate string for window title
            _windowTitle = string.Format("Socially - {0}", _community.Name);

            // Concatenate name and title to use as image tooltip
            _profilePhotoToolTip = string.Format("{0}, {1}", _community.Profile.Name, _community.Profile.Title);

        }

        public Community Community { get { return _community; } }
        public string WindowTitle { get { return _windowTitle; } }
        public string ProfilePhotoToolTip { get { return _profilePhotoToolTip; } }
    }
}