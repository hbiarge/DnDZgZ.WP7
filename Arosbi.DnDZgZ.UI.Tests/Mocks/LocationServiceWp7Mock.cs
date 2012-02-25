namespace Arosbi.DnDZgZ.UI.Tests.Mocks
{
    using System;
    using System.Device.Location;
    using System.Reactive.Subjects;

    using WP7Contrib.Services.Location;

    public class LocationServiceWp7Mock : ILocationService
    {
        private Subject<GeoCoordinate> location;
        private Subject<GeoPositionStatus> status;

        public LocationServiceWp7Mock()
        {
            location = new Subject<GeoCoordinate>();
            status = new Subject<GeoPositionStatus>();
        }

        public IObservable<GeoCoordinate> LocationByTimeThreshold(int frequency)
        {
            return this.location;
        }

        public IObservable<GeoCoordinate> LocationByTimeThreshold(int frequency, GeoPositionAccuracy accuracy)
        {
            return this.location;
        }

        public IObservable<GeoCoordinate> LocationByTimeThreshold(TimeSpan frequency)
        {
            return this.location;
        }

        public IObservable<GeoCoordinate> LocationByTimeThreshold(TimeSpan frequency, GeoPositionAccuracy accuracy)
        {
            return this.location;
        }

        public IObservable<GeoCoordinate> LocationByDistanceThreshold(int distance)
        {
            return this.location;
        }

        public IObservable<GeoCoordinate> LocationByDistanceThreshold(int distance, GeoPositionAccuracy accuracy)
        {
            return this.location;
        }

        public IObservable<GeoPositionStatus> Status()
        {
            return this.status;
        }

        public IObservable<GeoCoordinate> Location()
        {
            return this.location;
        }

        public IObservable<GeoCoordinate> Location(GeoPositionAccuracy accuracy)
        {
            return this.location;
        }

        public IObservable<GeoCoordinate> Location(TimeSpan locationTimeout)
        {
            return this.location;
        }

        public IObservable<GeoCoordinate> Location(GeoPositionAccuracy accuracy, TimeSpan locationTimeout)
        {
            return this.location;
        }

        public void ChangeLocation(GeoCoordinate newLocation)
        {
            this.location.OnNext(newLocation);
        }

        public void ChangeStatus(GeoPositionStatus newStatus)
        {
            this.status.OnNext(newStatus);
        }
    }
}
