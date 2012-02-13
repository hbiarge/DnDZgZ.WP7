using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Arosbi.DnDZgZ.UI.Tests.Tests.Mocks
{
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
