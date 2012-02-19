// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusesViewModel.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the BusesViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using GalaSoft.MvvmLight;

namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Device.Location;
    using System.Linq;
    using System.Windows.Input;

    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services;

    using GalaSoft.MvvmLight.Command;

    using Microsoft.Phone.Controls.Maps;

    public class BusesViewModel : ViewModelBase
    {
        /// <summary>
        /// Maximum map zoom level allowed.
        /// </summary>
        internal const double MaxZoomLevel = 19.0;

        /// <summary>
        /// Minimum map zoom level allowed.
        /// </summary>
        internal const double MinZoomLevel = 16.0;

        /// <summary>
        /// Default map zoom level.
        /// </summary>
        internal const double DefaultZoomLevel = 18.0;

        /// <summary>
        /// The device location service.
        /// </summary>
        private readonly ILocationService locationService;

        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository repository;

        /// <summary>
        /// Collection of pushpins available on map.
        /// </summary>
        private readonly ObservableCollection<PushpinModel> busStops = new ObservableCollection<PushpinModel>();

        /// <summary>
        /// Command for the ZoomIn button.
        /// </summary>
        private RelayCommand zoomInCommand;

        /// <summary>
        /// Command for the ZoomOut button.
        /// </summary>
        private RelayCommand zoomOutCommand;

        /// <summary>
        /// Command for the pushpins.
        /// </summary>
        private RelayCommand<string> pushpinCommand;

        /// <summary>
        /// Command that closes the popup.
        /// </summary>
        private RelayCommand closePopupCommand;

        /// <summary>
        /// Map zoom level.
        /// </summary>
        private double zoom;

        /// <summary>
        /// Map center coordinate.
        /// </summary>
        private GeoCoordinate center;

        /// <summary>
        /// The state of the info popup.
        /// </summary>
        private bool isPopupOpen;

        /// <summary>
        /// Info of the current bus stop.
        /// </summary>
        private BusDetail currentBusInfo;

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
                this.CurrentBusInfo = new BusDetail
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

            this.locationService.Start();
            this.locationService.LocationChanged += this.OnCurrentLocationChanged;

            this.InitializeDefaults();
        }

        /// <summary>
        /// Gets the credentials provider for the map control.
        /// </summary>
        public CredentialsProvider CredentialsProvider
        {
            get { return ViewModelLocator.CredentialsProvider; }
        }

        /// <summary>
        /// Gets or sets the map zoom level.
        /// </summary>
        public double Zoom
        {
            get
            {
                return this.zoom;
            }
            internal set
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

        /// <summary>
        /// Gets or sets the map center location coordinate.
        /// </summary>
        public GeoCoordinate Center
        {
            get
            {
                return this.center;
            }
            internal set
            {
                if (this.center != value)
                {
                    this.center = value;
                    this.RaisePropertyChanged("Center");
                }
            }
        }

        /// <summary>
        /// Gets a collection of pushpins.
        /// </summary>
        public ObservableCollection<PushpinModel> BusStops
        {
            get
            {
                return this.busStops;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the popup is open.
        /// </summary>
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

        public BusDetail CurrentBusInfo
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

        public ICommand PushpinCommand
        {
            get
            {
                if (this.pushpinCommand == null)
                {
                    this.pushpinCommand = new RelayCommand<string>(
                        p => this.repository.GetBusDetails(
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

        /// <summary>
        /// Set defaults.
        /// </summary>
        private void InitializeDefaults()
        {
            this.Zoom = DefaultZoomLevel;
            this.Center = this.GetCenter();

            this.repository.GetBuses(buses =>
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
            var currentLocation = this.locationService.GetCurrentLocation();

            if (currentLocation != null)
            {
                return new GeoCoordinate(currentLocation.Latitude, currentLocation.Longitude);
            }

            return ViewModelLocator.DefaultLocation;
        }

        private void OnCurrentLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (e.Location == null)
            {
                return;
            }

            this.Center = new GeoCoordinate(e.Location.Latitude, e.Location.Longitude);
        }

        public override void Cleanup()
        {
            // Clean own resources if needed
            this.locationService.LocationChanged -= this.OnCurrentLocationChanged;
            this.locationService.Stop();

            base.Cleanup();
        }
    }
}