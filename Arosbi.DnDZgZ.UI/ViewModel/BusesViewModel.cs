// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusesViewModel.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the BusesViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Device.Location;
    using System.Linq;
    using System.Reactive.Concurrency;
    using System.Windows;
    using System.Windows.Input;

    using Arosbi.DnDZgZ.UI.Model;
    using Arosbi.DnDZgZ.UI.Services;

    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    using Microsoft.Phone.Controls.Maps;

    using ILocationService = WP7Contrib.Services.Location.ILocationService;

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
        /// The location service.
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

        private IDisposable locationSuscription;

        /// <summary>
        /// Command for the ZoomIn button.
        /// </summary>
        private RelayCommand zoomInCommand;

        /// <summary>
        /// Command for the ZoomOut button.
        /// </summary>
        private RelayCommand zoomOutCommand;

        /// <summary>
        /// Command that gets the current location.
        /// </summary>
        private RelayCommand currentLocationCommand;

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
        /// The current location provided by the GPS.
        /// </summary>
        private GeoCoordinate currentLocation;

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
                        id = "123",
                        service = "Bus",
                        title = "123",
                        items = new string[][]
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

        /// <summary>
        /// Gets or sets the current location coordinate.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the map center location coordinate.
        /// </summary>
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
                                Location = new GeoCoordinate(b.lat, b.lon),
                                Id = b.id
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