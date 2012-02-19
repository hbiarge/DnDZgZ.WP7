namespace Arosbi.DnDZgZ.UI.Tests.Mocks
{
    using System;
    using System.Windows.Navigation;

    using Arosbi.DnDZgZ.UI.Services;

    public class NavigationServiceMock : INavigationService
    {
        public event NavigatingCancelEventHandler Navigating;

        public void NavigateTo(Uri pageUri)
        {
            this.LastNavigateToUri = pageUri;
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public Uri LastNavigateToUri { get; private set; }
    }
}
