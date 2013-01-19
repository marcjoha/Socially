using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ScApi.Data;

namespace Socially.ViewModels
{
    public class MessageViewModel
    {
        private readonly Message _message;

        public MessageViewModel(Message message)
        {
            _message = message;
        }

        public string ProfilePhotoUrl
        {
            get { return _message.User.Avatars.Square45; }
        }

        public string ProfilePhotoToolTip
        {
            get { return string.Format("{0}, {1}", _message.User.Name, _message.User.Title); }
        }

        public string Name
        {
            get { return _message.User.Name; }
        }

        public string Action
        {
            get
            {
                if (_message.Groups.Count > 0)
                {
                    string action = " posted to ";
                    for (int i = 0; i < _message.Groups.Count - 1; i++)
                    {
                        action += ", " + _message.Groups[i].Groupname;
                    }
                    if (_message.Groups.Count > 1)
                    {
                        return action + " and " + _message.Groups[_message.Groups.Count - 1].Groupname;
                    }
                    else
                    {
                        return action + _message.Groups[0].Groupname;
                    }
                }

                return null;
            }
        }

        public string Text
        {
            get { return _message.Body; }
        }

        public ObservableCollection<ExternalResourceViewModel> ExternalResources
        {
            get
            {
                List<ExternalResourceViewModel> list =
                    _message.ExternalResources.Select(eR => new ExternalResourceViewModel(eR)).ToList();
                return new ObservableCollection<ExternalResourceViewModel>(list);
            }
        }

        public ObservableCollection<MediaFileViewModel> MediaFiles
        {
            get
            {
                var list = _message.MediaFiles.Select(mF => new MediaFileViewModel(mF)).ToList();
                return new ObservableCollection<MediaFileViewModel>(list);
            }
        }
    }
}