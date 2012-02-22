namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Microsoft.Phone.Tasks;

    using NorthernLights;

    using WP7Contrib.Services.Navigation;

    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        private RelayCommand autobusesCommand;
        private RelayCommand biziCommand;
        private RelayCommand wifiCommand;
        private RelayCommand aboutCommand;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation Service.
        /// </param>
        public MainViewModel(INavigationService navigationService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            this.navigationService = navigationService;

            this.CheckForUnhandledExceptions();
        }

        public string ApplicationTitle
        {
            get
            {
                return "DnDZgZ";
            }
        }

        public string Autobuses
        {
            get
            {
                return "Autobuses";
            }
        }

        public ICommand AutobusesCommand
        {
            get
            {
                if (this.autobusesCommand == null)
                {
                    this.autobusesCommand =
                        new RelayCommand(() => this.navigationService.Navigate(ViewModelLocator.BusesPage));
                }

                return this.autobusesCommand;
            }
        }

        public string Bizi
        {
            get
            {
                return "Bizi";
            }
        }

        public ICommand BiziCommand
        {
            get
            {
                if (this.biziCommand == null)
                {
                    this.biziCommand =
                        new RelayCommand(() => this.navigationService.Navigate(ViewModelLocator.BizisPage));
                }

                return this.biziCommand;
            }
        }

        public string Wifi
        {
            get
            {
                return "Wifi";
            }
        }

        public ICommand WifiCommand
        {
            get
            {
                if (this.wifiCommand == null)
                {
                    this.wifiCommand =
                        new RelayCommand(() => this.navigationService.Navigate(ViewModelLocator.WifiPage));
                }

                return this.wifiCommand;
            }
        }

        public string About
        {
            get
            {
                return "Acerca de...";
            }
        }

        public ICommand AboutCommand
        {
            get
            {
                if (this.aboutCommand == null)
                {
                    this.aboutCommand =
                        new RelayCommand(
                            () =>
                            this.navigationService.Navigate(
                                new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative)));
                }

                return this.aboutCommand;
            }
        }

        private void CheckForUnhandledExceptions()
        {
            var exception = LittleWatson.GetPreviousException();

            if (exception == null)
            {
                return;
            }

            if (LittleWatson.Instance.AllowAnonymousHttpReporting)
            {
                const string PostUri = "http://www.yourdomain.com/data/post.php";
                LittleWatson.Instance.SendExceptionToHttpEndpoint(PostUri, exception);
            }
            else
            {
                // show popup.
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var email = new EmailComposeTask
                    {
                        To = "error@arosbi.com.com",
                        Subject = "DnDZgZ: auto-generated problem report",
                        Body = exception.Message + Environment.NewLine + exception.StackTrace
                    };

                    email.Show();
                });
            }
        }
    }
}