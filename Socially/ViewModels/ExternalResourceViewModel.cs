using ScApi.Data;

namespace Socially.ViewModels
{
    public class ExternalResourceViewModel
    {
        private readonly ExternalResource _externalResource;

        public ExternalResourceViewModel(ExternalResource externalResource)
        {
            _externalResource = externalResource;
        }

        public string Title { get { return _externalResource.Title; } }

        public string Url { get { return _externalResource.Url; } }

        public string Description { get { return _externalResource.Description; } }

        public string ThumbnailUrl
        {
            get { return _externalResource.OEmbed.ThumbnailUrl; }
        }
    }
}