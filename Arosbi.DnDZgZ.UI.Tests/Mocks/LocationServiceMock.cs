namespace Arosbi.DnDZgZ.UI.Tests.Mocks
{
    using System;
    using System.Device.Location;

    using Arosbi.DnDZgZ.UI.Services;
    using Arosbi.DnDZgZ.UI.ViewModel;

    public class LocationServiceMock : ILocationService
    {
        private GeoCoordinate location = new GeoCoordinate(40, 0);

        private readonly CurrentLocationType locationType;

        public event EventHandler<LocationChangedEventArgs> LocationChanged;

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

        public LocationServiceMock(CurrentLocationType locationType)
        {
            this.locationType = locationType;
        }

        /// <summary>
        /// Gets a value indicating whether the service is started.
        /// </summary>
        public bool IsStarted { get; private set; }

        /// <summary>
        /// Gets the current location.
        /// </summary>
        /// <returns>
        /// The current location represented as <see cref="ILocation"/>. If no location is available, <c>null</c> will be returned.
        /// </returns>
        public ILocation GetCurrentLocation()
        {
            if (this.locationType == CurrentLocationType.NullLocation)
            {
                return null;
            }

            return new Location(this.location);
        }

        /// <summary>
        /// Starts the location service so it's retrieving data.
        /// </summary>
        public void Start()
        {
            this.IsStarted = true;
        }

        /// <summary>
        /// Stops the location service so it's no longer retrieving data.
        /// </summary>
        public void Stop()
        {
            this.IsStarted = false;
        }

        public void ChangeLocation(GeoCoordinate newGeoCoordinate)
        {
            this.location = newGeoCoordinate;

            if (this.LocationChanged != null)
            {
                var newLocation = new Location(newGeoCoordinate);
                this.LocationChanged(this, new LocationChangedEventArgs(newLocation));
            }
        }
    }
}
