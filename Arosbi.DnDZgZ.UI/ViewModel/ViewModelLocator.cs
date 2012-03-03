namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Device.Location;
    using System.Windows;

    using Arosbi.DnDZgZ.UI.Services;
    using Arosbi.DnDZgZ.UI.Services.Fakes;

    using Funq;

    using GalaSoft.MvvmLight;

    using Microsoft.Phone.Controls.Maps;

    using WP7Contrib.Caching;
    using WP7Contrib.Communications;
    using WP7Contrib.Logging;
    using WP7Contrib.Services.Location;
    using WP7Contrib.Services.Navigation;

    public class ViewModelLocator
    {
        //private const string MapId = "";
        private const string MapId = "replace-with-your-private-key";

        internal static readonly CredentialsProvider CredentialsProvider = new ApplicationIdCredentialsProvider(MapId);

        internal static readonly GeoCoordinate DefaultLocation = new GeoCoordinate(41.6521, -0.8809);

        internal static readonly Uri BusesPage = new Uri("/BusesPage.xaml", UriKind.Relative);
        internal static readonly Uri BizisPage = new Uri("/BizisPage.xaml", UriKind.Relative);
        internal static readonly Uri WifiPage = new Uri("/WifisPage.xaml", UriKind.Relative);

        private static Funq.Container container;
        
        private static MainViewModel main;
        private static BusesViewModel buses;

        static ViewModelLocator()
        {
            InitializeContainer();
        }

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

        public static void ClearMain()
        {
            main.Cleanup();
            main = null;
        }

        public static void CreateMain()
        {
            if (main == null)
            {
                main = container.Resolve<MainViewModel>();
            }
        }

        #endregion

        #region Buses

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

        public static void ClearBuses()
        {
            if (buses == null)
            {
                return;
            }

            buses.Cleanup();
            buses = null;
        }

        public static void CreateBuses()
        {
            if (buses == null)
            {
                buses = container.Resolve<BusesViewModel>();
            }
        }

        #endregion

        public static void Cleanup()
        {
            ClearMain();
            ClearBuses();

            container.Dispose();
        }

        private static void InitializeContainer()
        {
            container = new Container();

#if DEBUG

            container.Register<ISerializer>(c => new JsonSerializer());

            container.Register<ILog>(c => new ConsoleLog());

            // Navigation service es un singleton
            container.Register<INavigationService>(c => 
                new ApplicationFrameNavigationService(((App)Application.Current).RootFrame))
                    .ReusedWithin(ReuseScope.Container);
            
            container.Register<ILocationService>(c => new LocationService(
                GeoPositionAccuracy.Default,
                container.Resolve<ILog>()));

            container.Register<IRepository>(c => new FakeRepository(
                                                     container.Resolve<ISerializer>()));

            container.Register<MainViewModel>(c => new MainViewModel(
                                                       container.LazyResolve<INavigationService>()));
            container.Register<BusesViewModel>(c => new BusesViewModel(
                                                        container.Resolve<ILocationService>(),
                                                        container.Resolve<IRepository>()));
#else

            container.Register<ISerializer>(c => new JsonSerializer());

            container.Register<ILog>(c => new NullLoggingService());

            //container.Register<ICacheProvider>(new IsolatedStorageCacheProvider());

            // Navigation service es un singleton
            container.Register<INavigationService>(c => 
                new ApplicationFrameNavigationService(((App)Application.Current).RootFrame))
                    .ReusedWithin(ReuseScope.Container);
            
            container.Register<ILocationService>(c => new LocationService(
                GeoPositionAccuracy.Default,
                container.Resolve<ILog>()));

            container.Register<IRepository>(c =>new RestRepository(
                                                     container.Resolve<ISerializer>()));

            container.Register<MainViewModel>(c => new MainViewModel(
                                                       container.LazyResolve<INavigationService>()));
            container.Register<BusesViewModel>(c => new BusesViewModel(
                                                        container.Resolve<ILocationService>(),
                                                        container.Resolve<IRepository>()));

#endif
        }
    }
}