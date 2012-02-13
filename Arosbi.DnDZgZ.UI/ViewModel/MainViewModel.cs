namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Navigation;

    using Arosbi.DnDZgZ.UI.Services;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        private RelayCommand autobusesCommand;
        private RelayCommand biziCommand;
        private RelayCommand wifiCommand;

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
                        new RelayCommand(() => this.navigationService.NavigateTo(ViewModelLocator.BusesPage));
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
                        new RelayCommand(() => this.navigationService.NavigateTo(ViewModelLocator.BizisPage));
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
                        new RelayCommand(() => this.navigationService.NavigateTo(ViewModelLocator.WifiPage));
                }

                return this.wifiCommand;
            }
        }
    }
}