using Socially.Models;

// denna klassen får bara se Socially.Models, inte scapi!

namespace Socially.ViewModels
{
    public class MainViewModel
    {
        private string _windowTitle;
        private Socialcast _socialcast;

        public MainViewModel()
        {
            _socialcast = new Socialcast("https://demo.socialcast.com", "emily@socialcast.com", "demo");
            
            // Concatenate string for window title
            _windowTitle = string.Format("Socially - {0}", _socialcast.Community.Name);

        }

        public string WindowTitle { get { return _windowTitle; } }
    }
}