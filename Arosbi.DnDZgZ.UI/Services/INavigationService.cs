using System;
using System.Windows.Navigation;

namespace Arosbi.DnDZgZ.UI.Services
{
    public interface INavigationService
    {
        event NavigatingCancelEventHandler Navigating;
        void NavigateTo(Uri pageUri);
        void GoBack();
    }
}
