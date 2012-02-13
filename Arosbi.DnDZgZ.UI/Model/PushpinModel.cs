// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PushpinModel.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Represents a pushpin data model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Arosbi.DnDZgZ.UI.Model
{
    using System.Device.Location;

    /// <summary>
    /// Represents a pushpin data model.
    /// </summary>
    public class PushpinModel
    {
        /// <summary>
        /// Gets or sets the pushpin location.
        /// </summary>
        public GeoCoordinate Location { get; set; }

        /// <summary>
        /// Gets or sets the pushpin icon uri.
        /// </summary>
        public Uri Icon { get; set; }
    }
}
