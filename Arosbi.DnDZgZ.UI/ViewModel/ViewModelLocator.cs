﻿/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Arosbi.DnDZgZ.UI.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
  
  OR (WPF only):
  
  xmlns:vm="clr-namespace:Arosbi.DnDZgZ.UI.ViewModel"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/

namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Device.Location;
    using System.Windows;

    using Arosbi.DnDZgZ.UI.Services;
    using Arosbi.DnDZgZ.UI.Services.Fakes;

    using GalaSoft.MvvmLight;

    using Microsoft.Phone.Controls.Maps;

    using WP7Contrib.Communications;
    using WP7Contrib.Services.Navigation;

    public class ViewModelLocator
    {
        /// <summary>
        /// Credentials for the map control.
        /// </summary>
        internal static readonly CredentialsProvider CredentialsProvider = new ApplicationIdCredentialsProvider(MapId);

        /// <summary>
        /// Default location coordinate.
        /// </summary>
        internal static readonly GeoCoordinate DefaultLocation = new GeoCoordinate(41.6521, -0.8809);

        internal static readonly Uri BusesPage = new Uri("/BusesPage.xaml", UriKind.Relative);
        internal static readonly Uri BizisPage = new Uri("/BizisPage.xaml", UriKind.Relative);
        internal static readonly Uri WifiPage = new Uri("/WifisPage.xaml", UriKind.Relative);

        private static Funq.Container container;

        /// <summary>
        /// Registered ID used to access map control and Bing maps service.
        /// </summary>
        private const string MapId = "replace-with-your-private-key";

        private static MainViewModel main;
        private static BusesViewModel buses;

        static ViewModelLocator()
        {
            container = new Funq.Container();

            container.Register<ISerializer>(c => new JsonContractSerializer());

            container.Register<INavigationService>(c => new ApplicationFrameNavigationService(((App)Application.Current).RootFrame));
            container.Register<ILocationService>(c => new LocationService());
            
            container.Register<IRepository>(c => new FakeRepository(
                container.Resolve<ISerializer>()));

            container.Register<MainViewModel>(c => new MainViewModel(
                container.Resolve<INavigationService>()));
            container.Register<BusesViewModel>(c => new BusesViewModel(
                container.Resolve<ILocationService>(),
                container.Resolve<IRepository>()));

        }

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view models
            }
            else
            {
                // Create run time view models
            }

            CreateMain();
        }

        #region Main
        /// <summary>
        /// Gets the Main property.
        /// </summary>
        public static MainViewModel MainStatic
        {
            get
            {
                if (main == null)
                {
                    CreateMain();
                }

                return main;
            }
        }

        /// <summary>
        /// Gets the Main property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return MainStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the Main property.
        /// </summary>
        public static void ClearMain()
        {
            main.Cleanup();
            main = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the Main property.
        /// </summary>
        public static void CreateMain()
        {
            if (main == null)
            {
                main = container.Resolve<MainViewModel>();
            }
        }

        #endregion

        #region Buses

        /// <summary>
        /// Gets the $ViewModelPropertyName$ property.
        /// </summary>
        public static BusesViewModel BusesStatic
        {
            get
            {
                if (buses == null)
                {
                    CreateBuses();
                }

                return buses;
            }
        }

        /// <summary>
        /// Gets the Buses property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public BusesViewModel Buses
        {
            get
            {
                return BusesStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the $ViewModelPropertyName$ property.
        /// </summary>
        public static void ClearBuses()
        {
            if (buses == null)
            {
                return;
            }

            buses.Cleanup();
            buses = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the $ViewModelPropertyName$ property.
        /// </summary>
        public static void CreateBuses()
        {
            if (buses == null)
            {
                buses = container.Resolve<BusesViewModel>();
            }
        }

        #endregion

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearMain();
            ClearBuses();

            container.Dispose();
        }
    }
}