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
        /// Default map zoom level.
        /// </summary>
        private const double DefaultZoomLevel = 18.0;

        /// <summary>
        /// Maximum map zoom level allowed.
        /// </summary>
        private const double MaxZoomLevel = 20.0;

        /// <summary>
        /// Minimum map zoom level allowed.
        /// </summary>
        private const double MinZoomLevel = 15.0;

        private readonly INavigationService navigationService;

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
        /// Map zoom level.
        /// </summary>
        private double zoom;

        /// <summary>
        /// Map center coordinate.
        /// </summary>
        private GeoCoordinate center;

        public BusesViewModel(INavigationService navigationService, IRepository repository)
        {
            if (navigationService == null)
            {
                throw new ArgumentNullException("navigationService");
            }

            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            this.navigationService = navigationService;
            this.repository = repository;

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
            get { return this.busStops; }
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

        /// <summary>
        /// Set defaults.
        /// </summary>
        private void InitializeDefaults()
        {
            this.Zoom = DefaultZoomLevel;
            this.Center = ViewModelLocator.DefaultLocation;
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}