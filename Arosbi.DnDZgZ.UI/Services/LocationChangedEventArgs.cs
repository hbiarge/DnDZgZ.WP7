// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocationChangedEventArgs.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   <see cref="EventArgs" /> implementation which contains a location.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Arosbi.DnDZgZ.UI.Services
{
    /// <summary>
    /// <see cref="EventArgs"/> implementation which contains a location.
    /// </summary>
    public class LocationChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationChangedEventArgs"/> class.
        /// </summary>
        /// <param name="newLocation">The new location.</param>
        public LocationChangedEventArgs(ILocation newLocation)
        {
            this.Location = newLocation;
        }

        /// <summary>
        /// Gets the new location.
        /// </summary>
        /// <value>The new location.</value>
        public ILocation Location { get; private set; }
    }

}
