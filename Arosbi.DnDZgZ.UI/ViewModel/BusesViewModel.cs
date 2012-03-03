namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Device.Location;
    using System.Linq;
    using System.Windows.Input;

    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Microsoft.Phone.Controls.Maps;

    using WP7Contrib.Collections;
    using WP7Contrib.Common.Extensions;

    using ILocationService = WP7Contrib.Services.Location.ILocationService;

    public class BusesViewModel : ViewModelBase
    {
        internal const double MaxZoomLevel = 19.0;

        internal const double MinZoomLevel = 16.0;

        internal const double DefaultZoomLevel = 18.0;

        private readonly ILocationService locationService;

        private readonly IRepository repository;

        private readonly TrickleAllToCollection<PushpinModel> trikler;

        private readonly List<PushpinModel> cachedBusStops = new List<PushpinModel>();

        private readonly System.Collections.ObjectModel.ObservableCollection<PushpinModel> showedBusStops = new System.Collections.ObjectModel.ObservableCollection<PushpinModel>();

        private RelayCommand zoomInCommand;

        private RelayCommand zoomOutCommand;

        private RelayCommand findCurrentLocationCommand;

        private RelayCommand<string> showBusStopDetailsCommand;

        private RelayCommand hideBusStopDetailsCommand;

        private double zoomLevelLevel;

        private GeoCoordinate currentLocation;

        private GeoCoordinate center;

        private bool showBusStopDetail;

        private BusStopDetail currentBusStopDetailses;

        private string findingLocationMessage;

        private bool isFindingLocation;

        private string findingInfoMessage;

        private bool isFindingInfo;

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
                this.CurrentBusStopDetails = new BusStopDetail
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
            this.trikler = new TrickleAllToCollection<PushpinModel>(
                () => this.ToString(),
                () => this.IsFindingInfo = false);

            this.findingLocationMessage = "Obteniendo posición actual...";
            this.IsFindingLocation = true;

            locationService.Location(GeoPositionAccuracy.High, TimeSpan.FromSeconds(20))
                .Subscribe(
                    location =>
                    {
                        this.IsFindingLocation = false;
                        this.CurrentLocation = this.Center = location;
                    });

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

        public System.Collections.ObjectModel.ObservableCollection<PushpinModel> BusStops
        {
            get
            {
                return this.showedBusStops;
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

        public BusStopDetail CurrentBusStopDetails
        {
            get
            {
                return this.currentBusStopDetailses;
            }
            private set
            {
                this.currentBusStopDetailses = value;
                this.RaisePropertyChanged("CurrentBusStopDetails");
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

        public ICommand FindCurrentLocationCommand
        {
            get
            {
                if (this.findCurrentLocationCommand == null)
                {
                    this.findCurrentLocationCommand = new RelayCommand(
                        () =>
                        {
                            this.FindingLocationMessage = "Oteniendo posición...";
                            this.IsFindingLocation = true;

                            this.locationService.Location(GeoPositionAccuracy.High, TimeSpan.FromSeconds(20))
                                .Subscribe(
                                    location =>
                                    {
                                        this.IsFindingLocation = false;
                                        this.CurrentLocation = this.Center = location;
                                    });
                        });
                }

                return this.findCurrentLocationCommand;
            }
        }

        public ICommand ShowBusStopDetailsCommand
        {
            get
            {
                if (this.showBusStopDetailsCommand == null)
                {
                    this.showBusStopDetailsCommand = new RelayCommand<string>(
                        busStopId =>
                        {
                            this.FindingInfoMessage = string.Format("Cargando poste {0}...", busStopId);
                            this.IsFindingInfo = true;

                            this.repository.GetBusStopDetails(
                                busStopId,
                                busDetails =>
                                {
                                    this.IsFindingInfo = false;

                                    this.CurrentBusStopDetails = busDetails;
                                    this.ShowBusStopDetail = true;
                                });
                        });
                }

                return this.showBusStopDetailsCommand;
            }
        }

        public ICommand HideBusStopDetailsCommand
        {
            get
            {
                if (this.hideBusStopDetailsCommand == null)
                {
                    this.hideBusStopDetailsCommand = new RelayCommand(() =>
                        {
                            this.CurrentBusStopDetails = null;
                            this.ShowBusStopDetail = false;
                        });
                }

                return this.hideBusStopDetailsCommand;
            }
        }

        public string FindingLocationMessage
        {
            get
            {
                return this.findingLocationMessage;
            }
            internal set
            {
                this.findingLocationMessage = value;
                RaisePropertyChanged("FindingLocationMessage");
            }
        }

        public bool IsFindingLocation
        {
            get
            {
                return this.isFindingLocation;
            }
            internal set
            {
                this.isFindingLocation = value;
                RaisePropertyChanged("IsFindingLocation");
            }
        }

        public string FindingInfoMessage
        {
            get
            {
                return this.findingInfoMessage;
            }
            internal set
            {
                this.findingInfoMessage = value;
                RaisePropertyChanged("FindingInfoMessage");
            }
        }

        public bool IsFindingInfo
        {
            get
            {
                return this.isFindingInfo;
            }
            internal set
            {
                this.isFindingInfo = value;
                RaisePropertyChanged("IsFindingInfo");
            }
        }

        public void LoadBusStopsInExtents(LocationRect extents)
        {
            if (this.cachedBusStops.Count == 0)
            {
                this.GetBusStops(extents, this.LoadBusStopsInExtentsImpl);
            }
            else
            {
                this.LoadBusStopsInExtentsImpl(extents);
            }
        }

        private void GetBusStops(LocationRect extents, Action<LocationRect> callback)
        {
            this.FindingInfoMessage = "Obteniendo postes...";
            this.IsFindingInfo = true;

            this.repository.GetBusStops(buses =>
                {
                    var busStops = buses
                        .Select(b => new PushpinModel
                            {
                                Location = new GeoCoordinate(b.Lat, b.Lon),
                                Id = b.Id
                            });

                    this.cachedBusStops.AddRange(busStops);

                    this.IsFindingInfo = false;
                    callback(extents);
                });
        }

        private void LoadBusStopsInExtentsImpl(LocationRect extents)
        {
            this.trikler.Stop();

            this.FindingInfoMessage = "Añadiendo postes...";
            this.IsFindingInfo = true;

            // All the pins to add to the map control...
            var allPinsToAdd = this.cachedBusStops
                .Where(bs => (bs.Location.Latitude <= extents.North) &&
                             (bs.Location.Latitude >= extents.South) &&
                             (bs.Location.Longitude >= extents.West) &&
                             (bs.Location.Longitude <= extents.East)).ToArray();

            // All the pins already added to the map control we want to keep...
            var alreadyAdded = allPinsToAdd.Intersect(this.showedBusStops).ToArray();

            // The new pins to be added to the mapp control...
            var pinsToAdd = allPinsToAdd.Except(alreadyAdded).ToArray();

            // The existing pins to be removed which aren't visible...
            var pinsToRemove = this.showedBusStops.Except(alreadyAdded).ToArray();

            // Remove the pins...
            pinsToRemove.ForEach(p => this.showedBusStops.Remove(p));

            this.trikler.Start(10, pinsToAdd, this.showedBusStops);
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