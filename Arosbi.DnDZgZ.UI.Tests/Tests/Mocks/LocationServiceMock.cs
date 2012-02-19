using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Arosbi.DnDZgZ.UI.Tests.Tests.Mocks
{
    using Arosbi.DnDZgZ.UI.Services;

    public class LocationServiceMock:ILocationService
    {
        public event EventHandler<LocationChangedEventArgs> LocationChanged;

        /// <summary>
        /// Gets the current location represented as <see cref="ILocation"/>. If no location is available, <c>null</c> will be returned.
        /// </summary>
        /// <value>The current location.</value>
        public ILocation CurrentLocation
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the current location.
        /// </summary>
        /// <returns>
        /// The current location represented as <see cref="ILocation"/>. If no location is available, <c>null</c> will be returned.
        /// </returns>
        public ILocation GetCurrentLocation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts the location service so it's retrieving data.
        /// </summary>
        public void Start()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops the location service so it's no longer retrieving data.
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
