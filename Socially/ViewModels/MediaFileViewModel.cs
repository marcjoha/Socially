using ScApi.Data;

namespace Socially.ViewModels
{
    public class MediaFileViewModel
    {
        private readonly MediaFile _mediaFile;

        public MediaFileViewModel(MediaFile mediaFile)
        {
            _mediaFile = mediaFile;
        }

        public string ThumbnailUrl
        {
            get { return _mediaFile.Thumbnails.Stream; }
        }

        public string Url
        {
            get { return _mediaFile.Url; }
        }
    }
}