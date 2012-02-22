namespace Arosbi.DnDZgZ.UI.Tests.Mocks
{
    using System;
    using System.Device.Location;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;

    using WP7Contrib.Services.Location;

    public class LocationServiceWp7Mock : ILocationService
    {
        private Subject<GeoCoordinate> subject;
        private Subject<GeoPositionStatus> status;

        public LocationServiceWp7Mock()
        {
            subject = new Subject<GeoCoordinate>();
            status = new Subject<GeoPositionStatus>();
        }

        public IObservable<GeoCoordinate> LocationByTimeThreshold(int frequency)
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoCoordinate> LocationByTimeThreshold(int frequency, GeoPositionAccuracy accuracy)
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoCoordinate> LocationByTimeThreshold(TimeSpan frequency)
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoCoordinate> LocationByTimeThreshold(TimeSpan frequency, GeoPositionAccuracy accuracy)
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoCoordinate> LocationByDistanceThreshold(int distance)
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoCoordinate> LocationByDistanceThreshold(int distance, GeoPositionAccuracy accuracy)
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoPositionStatus> Status()
        {
            return this.status.Select(s => s);
        }

        public IObservable<GeoCoordinate> Location()
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoCoordinate> Location(GeoPositionAccuracy accuracy)
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoCoordinate> Location(TimeSpan locationTimeout)
        {
            return this.subject.Select(g => g);
        }

        public IObservable<GeoCoordinate> Location(GeoPositionAccuracy accuracy, TimeSpan locationTimeout)
        {
            return this.subject.Select(g => g);
        }

        public void ChangeLocation(GeoCoordinate newLocation)
        {
            this.subject.OnNext(newLocation);
        }

        public void ChangeStatus(GeoPositionStatus newStatus)
        {
            this.status.OnNext(newStatus);
        }
    }
}
