// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocationService.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Service that supports retrieving the current location.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Services
{
    using System;
    using System.Device.Location;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Service that supports retrieving the current location.
    /// </summary>
    public class LocationService : ILocationService
    {
        /// <summary>
        /// Stores the watcher.
        /// </summary>
        private readonly GeoCoordinateWatcher geoCoordinateWatcher;

        /// <summary>
        /// Stores a value thay indicates if the service has a location.
        /// </summary>
        private bool hasLocation;

        #region Constructor & destructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationService"/> class.
        /// </summary>
        public LocationService()
        {
            this.geoCoordinateWatcher = new GeoCoordinateWatcher();
            this.geoCoordinateWatcher.PositionChanged += this.OnGeoCoordinateWatcherPositionChanged;
            this.geoCoordinateWatcher.StatusChanged += this.OnGeoCoordinateWatcherStatusChanged;

            if (this.geoCoordinateWatcher.Status == GeoPositionStatus.Ready)
            {
                this.hasLocation = true;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// Occurs when the current location has changed.
        /// </summary>
        public event EventHandler<LocationChangedEventArgs> LocationChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current location represented as <see cref="ILocation"/>. If no location is available, <c>null</c> will be returned.
        /// </summary>
        /// <value>The current location.</value>
        public ILocation CurrentLocation
        {
            get
            {
                return this.GetCurrentLocation();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the current location.
        /// </summary>
        /// <returns>
        /// The current location represented as <see cref="ILocation"/>. If no location is available, <c>null</c> will be returned.
        /// </returns>
        public ILocation GetCurrentLocation()
        {
            ILocation currentLocation = null;

            if (this.hasLocation)
            {
                var currentCoordinate = this.geoCoordinateWatcher.Position;
                currentLocation = new Location(currentCoordinate.Location);
            }

            return currentLocation;
        }

        /// <summary>
        /// Starts the location service so it's retrieving data.
        /// </summary>
        public void Start()
        {
            this.geoCoordinateWatcher.TryStart(false, new TimeSpan(0, 0, 5));
        }

        /// <summary>
        /// Stops the location service so it's no longer retrieving data.
        /// </summary>
        public void Stop()
        {
            this.geoCoordinateWatcher.Stop();
        }

        /// <summary>
        /// Called when the position has changed, according to the <see cref="GeoCoordinateWatcher"/>.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Device.Location.GeoPositionChangedEventArgs&lt;System.Device.Location.GeoCoordinate&gt;"/> instance containing the event data.</param>
        private void OnGeoCoordinateWatcherPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            this.OnLocationChanged();
        }

        /// <summary>
        /// Called when the status has changed, according to the <see cref="GeoCoordinateWatcher"/>.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Device.Location.GeoPositionStatusChangedEventArgs"/> instance containing the event data.</param>
        private void OnGeoCoordinateWatcherStatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                case GeoPositionStatus.Initializing:
                case GeoPositionStatus.NoData:
                    this.hasLocation = false;
                    this.OnLocationChanged();
                    break;

                case GeoPositionStatus.Ready:
                    if (!this.hasLocation)
                    {
                        this.hasLocation = true;
                        this.OnLocationChanged();
                    }

                    break;
            }
        }

        /// <summary>
        /// Called when the current location has changed.
        /// </summary>
        private void OnLocationChanged()
        {
            if (this.LocationChanged == null)
            {
                return;
            }

            var value = new LocationChangedEventArgs(this.GetCurrentLocation());

            Deployment.Current.Dispatcher.BeginInvoke(() => this.LocationChanged(this, value));
        }

        #endregion
    }
}