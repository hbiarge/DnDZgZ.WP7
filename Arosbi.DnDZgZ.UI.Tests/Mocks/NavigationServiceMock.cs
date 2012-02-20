namespace Arosbi.DnDZgZ.UI.Tests.Mocks
{
    using System;
    using System.Windows.Navigation;

    using WP7Contrib.Services.Navigation;

    public class NavigationServiceMock : INavigationService
    {
        public Uri CurrentSource
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event NavigatingCancelEventHandler Navigating;

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public bool Navigate(Uri source)
        {
            this.LastNavigateUri = source;
            return true;
        }

        public bool CanGoBack
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Uri LastNavigateUri { get; private set; }
    }
}
