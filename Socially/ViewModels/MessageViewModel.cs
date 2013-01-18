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
            // Concatenate name and title to use as image tooltip
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
                    var action = " posted to ";
                    for(var i = 0; i < _message.Groups.Count-1; i++)
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
                List<ExternalResourceViewModel> list = _message.ExternalResources.Select(eR => new ExternalResourceViewModel(eR)).ToList<ExternalResourceViewModel>();
                return new ObservableCollection<ExternalResourceViewModel>(list);
            }
        }
    }
}