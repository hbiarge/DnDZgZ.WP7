namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Navigation;

    using Arosbi.DnDZgZ.UI.Services;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        private RelayCommand autobusesCommand;
        private RelayCommand biziCommand;
        private RelayCommand wifiCommand;

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
                        new RelayCommand(() => this.navigationService.NavigateTo(new Uri(ViewModelLocator.BizisPage)));
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
                        new RelayCommand(() => this.navigationService.NavigateTo(new Uri(ViewModelLocator.WifiPage)));
                }

                return this.wifiCommand;
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(INavigationService navigationService)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            this.navigationService = navigationService;

            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}