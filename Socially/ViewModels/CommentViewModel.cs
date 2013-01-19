using System;
using ScApi.Data;

namespace Socially.ViewModels
{
    public class CommentViewModel
    {
        private readonly Comment _comment;

        public CommentViewModel(Comment comment)
        {
            _comment = comment;
        }

        public string ProfilePhotoUrl
        {
            get { return _comment.User.Avatars.Square45; }
        }

        public string ProfilePhotoToolTip
        {
            get { return string.Format("{0}, {1}", _comment.User.Name, _comment.User.Title); }
        }

        public string Name
        {
            get { return _comment.User.Name; }
        }

        public string Text
        {
            get { return _comment.Text; }
        }

        public string CreatedAt
        {
            get
            {
                DateTime createdAt = _comment.CreatedAt.ToLocalTime();

                if (createdAt.Date == DateTime.Today)
                {
                    return string.Format("Today, {0}", createdAt.ToShortTimeString());
                }
                else if (createdAt.Date == DateTime.Today.AddDays(-1))
                {
                    return string.Format("Yesterday, {0}", createdAt.ToShortTimeString());
                }
                else
                {
                    return createdAt.ToShortDateString();
                }
            }
        }
    }
}