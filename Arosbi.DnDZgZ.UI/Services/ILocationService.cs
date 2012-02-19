using System;

namespace Arosbi.DnDZgZ.UI.Services
{
    using System.ComponentModel;

    /// <summary>
    /// Interface that supports retrieving the current location.
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Occurs when the current location has changed.
        /// </summary>
        event EventHandler<LocationChangedEventArgs> LocationChanged;

        /// <summary>
        /// Gets the current location represented as <see cref="ILocation"/>. If no location is available, <c>null</c> will be returned.
        /// </summary>
        /// <value>The current location.</value>
        ILocation CurrentLocation { get; }

        /// <summary>
        /// Gets the current location.
        /// </summary>
        /// <returns>
        /// The current location represented as <see cref="ILocation"/>. If no location is available, <c>null</c> will be returned.
        /// </returns>
        ILocation GetCurrentLocation();

        /// <summary>
        /// Starts the location service so it's retrieving data.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the location service so it's no longer retrieving data.
        /// </summary>
        void Stop();
    }

}
