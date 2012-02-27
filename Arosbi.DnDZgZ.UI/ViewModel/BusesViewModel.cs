namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Device.Location;
    using System.Linq;
    using System.Windows.Input;

    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Microsoft.Phone.Controls.Maps;

    using ILocationService = WP7Contrib.Services.Location.ILocationService;

    public class BusesViewModel : ViewModelBase
    {
        internal const double MaxZoomLevel = 19.0;

        internal const double MinZoomLevel = 16.0;

        internal const double DefaultZoomLevel = 18.0;

        private readonly ILocationService locationService;

        private readonly IRepository repository;

        private readonly ObservableCollection<PushpinModel> busStops = new ObservableCollection<PushpinModel>();

        private IDisposable locationSuscription;

        private RelayCommand zoomInCommand;

        private RelayCommand zoomOutCommand;

        private RelayCommand currentLocationCommand;

        private RelayCommand<string> pushpinCommand;

        private RelayCommand closePopupCommand;

        private double zoom;

        private GeoCoordinate currentLocation;

        private GeoCoordinate center;

        private bool isPopupOpen;

        private BusStopDetail currentBusInfo;

        public BusesViewModel(ILocationService locationService, IRepository repository)
        {
            if (locationService == null)
            {
                throw new ArgumentNullException("locationService");
            }

            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            if (IsInDesignMode)
            {
                this.CurrentBusInfo = new BusStopDetail
                    {
                        Id = "123",
                        Service = "Bus",
                        Title = "123",
                        Items = new string[][]
                            {
                                new string[] { "[Ci1] 7 min.", "Dirección CAMINO LAS TORRES" },
                                new string[] { "[Ci1] 14 min.", "Dirección CAMINO LAS TORRES" },
                                new string[] { "[20] 2 min.", "Dirección CAMINO LAS TORRES" },
                                new string[] { "[20] 20 min.", "Dirección CAMINO LAS TORRES" },
                            }
                    };
            }

            this.locationService = locationService;
            this.repository = repository;

            this.locationSuscription = locationService.Location()
                .Subscribe(
                    location => this.CurrentLocation = this.Center = location,
                    () => this.locationSuscription.Dispose());

            this.InitializeDefaults();
        }

        public CredentialsProvider CredentialsProvider
        {
            get { return ViewModelLocator.CredentialsProvider; }
        }

        public double Zoom
        {
            get
            {
                return this.zoom;
            }
            set
            {
                var coercedZoom = Math.Max(MinZoomLevel, Math.Min(MaxZoomLevel, value));

                if (this.zoom != coercedZoom)
                {
                    this.zoom = value;
                    this.RaisePropertyChanged("Zoom");
                    ((RelayCommand)this.ZoomInCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)this.ZoomOutCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public GeoCoordinate CurrentLocation
        {
            get
            {
                return this.currentLocation;
            }
            internal set
            {
                if (this.currentLocation != value)
                {
                    this.currentLocation = value;
                    this.RaisePropertyChanged("CurrentLocation");
                }
            }
        }

        public GeoCoordinate Center
        {
            get
            {
                return this.center;
            }
            set
            {
                if (this.center != value)
                {
                    this.center = value;
                    this.RaisePropertyChanged("Center");
                }
            }
        }

        public ObservableCollection<PushpinModel> BusStops
        {
            get
            {
                return this.busStops;
            }
        }

        public bool IsPopupOpen
        {
            get
            {
                return this.isPopupOpen;
            }
            internal set
            {
                if (value == this.isPopupOpen)
                {
                    return;
                }

                this.isPopupOpen = value;
                RaisePropertyChanged("IsPopupOpen");
            }
        }

        public BusStopDetail CurrentBusInfo
        {
            get
            {
                return this.currentBusInfo;
            }
            private set
            {
                this.currentBusInfo = value;
                this.RaisePropertyChanged("CurrentBusInfo");
            }
        }

        public ICommand ZoomInCommand
        {
            get
            {
                if (this.zoomInCommand == null)
                {
                    this.zoomInCommand = new RelayCommand(
                        () => this.Zoom += 1,
                        () => this.Zoom < MaxZoomLevel);
                }

                return this.zoomInCommand;
            }
        }

        public ICommand ZoomOutCommand
        {
            get
            {
                if (this.zoomOutCommand == null)
                {
                    this.zoomOutCommand = new RelayCommand(
                        () => this.Zoom -= 1,
                        () => this.Zoom > MinZoomLevel);
                }

                return this.zoomOutCommand;
            }
        }

        public ICommand CurrentLocationCommand
        {
            get
            {
                if (this.currentLocationCommand == null)
                {
                    this.currentLocationCommand = new RelayCommand(
                        () =>
                        {
                            this.locationSuscription = this.locationService.Location()
                                .Subscribe(
                                    location => this.CurrentLocation = this.Center = location,
                                    () => this.locationSuscription.Dispose());
                        });
                }

                return this.currentLocationCommand;
            }
        }

        public ICommand PushpinCommand
        {
            get
            {
                if (this.pushpinCommand == null)
                {
                    this.pushpinCommand = new RelayCommand<string>(
                        p => this.repository.GetBusStopDetails(
                            p,
                            busDetail =>
                            {
                                this.CurrentBusInfo = busDetail;
                                this.IsPopupOpen = true;
                            }));
                }

                return this.pushpinCommand;
            }
        }

        public ICommand ClosePopupCommand
        {
            get
            {
                if (this.closePopupCommand == null)
                {
                    this.closePopupCommand = new RelayCommand(() => this.IsPopupOpen = false);
                }

                return this.closePopupCommand;
            }
        }

        private void InitializeDefaults()
        {
            this.Zoom = DefaultZoomLevel;
            this.Center = this.GetCenter();

            this.repository.GetBusStops(buses =>
                {
                    var pushpins = buses
                        .Select(b => new PushpinModel
                            {
                                Location = new GeoCoordinate(b.Lat, b.Lon),
                                Id = b.Id
                            });

                    foreach (var pushpin in pushpins)
                    {
                        var p = pushpin;
                        this.busStops.Add(p);
                    }
                });
        }

        private GeoCoordinate GetCenter()
        {
            if (this.currentLocation != null)
            {
                return this.currentLocation;
            }

            return ViewModelLocator.DefaultLocation;
        }

        public override void Cleanup()
        {
            // Clean own resources if needed
            base.Cleanup();
        }
    }
}