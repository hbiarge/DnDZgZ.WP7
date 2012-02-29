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

        private readonly ObservableCollection<PushpinModel> showedBusStops = new ObservableCollection<PushpinModel>();

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

        private string asyncMessage;

        private bool isDoingAsyncWork;

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
            this.trikler = new TrickleAllToCollection<PushpinModel>(
                () => this.ToString(),
                () => this.ToString());

            locationService.Location()
                .Subscribe(
                    location => this.CurrentLocation = this.Center = location);

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
                            this.AsyncMessage = "Oteniendo posición...";
                            this.IsDoingAsyncWork = true;

                            this.locationService.Location()
                                .Subscribe(
                                    location =>
                                    {
                                        this.IsDoingAsyncWork = false;
                                        this.CurrentLocation = this.Center = location;
                                    });
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
                        busStopId =>
                        {
                            this.AsyncMessage = string.Format("Cargando poste {0}...", busStopId);
                            this.IsDoingAsyncWork = true;

                            this.repository.GetBusStopDetails(
                                busStopId,
                                busDetail =>
                                {
                                    this.IsDoingAsyncWork = false;

                                    this.CurrentBusStopDetail = busDetail;
                                    this.ShowBusStopDetail = true;
                                });
                        });
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
                    this.closePopupCommand = new RelayCommand(() =>
                        {
                            this.CurrentBusStopDetail = null;
                            this.ShowBusStopDetail = false;
                        });
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
                    this.loadedCommand = new RelayCommand(() => this.ToString());
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

        public string AsyncMessage
        {
            get
            {
                return this.asyncMessage;
            }
            internal set
            {
                this.asyncMessage = value;
                RaisePropertyChanged("AsyncMessage");
            }
        }

        public bool IsDoingAsyncWork
        {
            get
            {
                return this.isDoingAsyncWork;
            }
            internal set
            {
                this.isDoingAsyncWork = value;
                RaisePropertyChanged("IsDoingAsyncWork");
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
            this.repository.GetBusStops(buses =>
                {
                    var busStops = buses
                        .Select(b => new PushpinModel
                            {
                                Location = new GeoCoordinate(b.Lat, b.Lon),
                                Id = b.Id
                            });

                    this.cachedBusStops.AddRange(busStops);
                    callback(extents);
                });
        }

        private void LoadBusStopsInExtentsImpl(LocationRect extents)
        {
            this.trikler.Stop();

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