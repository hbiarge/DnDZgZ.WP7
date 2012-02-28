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

        private RelayCommand<string> busStopDetailCommand;

        private RelayCommand closePopupCommand;

        private RelayCommand loadedCommand;

        private RelayCommand unloadedCommand;

        private double zoomLevelLevel;

        private GeoCoordinate currentLocation;

        private GeoCoordinate center;

        private bool showBusStopDetail;

        private BusStopDetail currentBusStopDetail;

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
                this.CurrentBusStopDetail = new BusStopDetail
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

            this.ZoomLevel = DefaultZoomLevel;
            this.Center = this.GetCenter();
        }

        public CredentialsProvider CredentialsProvider
        {
            get { return ViewModelLocator.CredentialsProvider; }
        }

        public double ZoomLevel
        {
            get
            {
                return this.zoomLevelLevel;
            }
            set
            {
                var coercedZoom = Math.Max(MinZoomLevel, Math.Min(MaxZoomLevel, value));

                if (this.zoomLevelLevel != coercedZoom)
                {
                    this.zoomLevelLevel = value;
                    this.RaisePropertyChanged("ZoomLevel");
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

        public bool ShowBusStopDetail
        {
            get
            {
                return this.showBusStopDetail;
            }
            internal set
            {
                if (value == this.showBusStopDetail)
                {
                    return;
                }

                this.showBusStopDetail = value;
                RaisePropertyChanged("ShowBusStopDetail");
            }
        }

        public BusStopDetail CurrentBusStopDetail
        {
            get
            {
                return this.currentBusStopDetail;
            }
            private set
            {
                this.currentBusStopDetail = value;
                this.RaisePropertyChanged("CurrentBusStopDetail");
            }
        }

        public ICommand ZoomInCommand
        {
            get
            {
                if (this.zoomInCommand == null)
                {
                    this.zoomInCommand = new RelayCommand(
                        () => this.ZoomLevel += 1,
                        () => this.ZoomLevel < MaxZoomLevel);
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
                        () => this.ZoomLevel -= 1,
                        () => this.ZoomLevel > MinZoomLevel);
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
                if (this.busStopDetailCommand == null)
                {
                    this.busStopDetailCommand = new RelayCommand<string>(
                        p => this.repository.GetBusStopDetails(
                            p,
                            busDetail =>
                            {
                                this.CurrentBusStopDetail = busDetail;
                                this.ShowBusStopDetail = true;
                            }));
                }

                return this.busStopDetailCommand;
            }
        }

        public ICommand ClosePopupCommand
        {
            get
            {
                if (this.closePopupCommand == null)
                {
                    this.closePopupCommand = new RelayCommand(() => this.ShowBusStopDetail = false);
                }

                return this.closePopupCommand;
            }
        }

        public ICommand LoadedCommand
        {
            get
            {
                if (this.loadedCommand == null)
                {
                    this.loadedCommand = new RelayCommand(this.GetBusStops);
                }

                return this.loadedCommand;
            }
        }

        public ICommand UnloadedCommand
        {
            get
            {
                if (this.unloadedCommand == null)
                {
                    this.unloadedCommand = new RelayCommand(() => this.ShowBusStopDetail = false);
                }

                return this.unloadedCommand;
            }
        }

        private void GetBusStops()
        {
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
                        this.busStops.Add(pushpin);
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