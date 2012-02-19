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

namespace Arosbi.DnDZgZ.UI.Services
{
    /// <summary>
    /// Interface that represents a location.
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Gets the latitude. The latitute is the angular distance of that location south or north of the equator.
        /// </summary>
        /// <value>The latitude.</value>
        double Latitude { get; }

        /// <summary>
        /// Gets the longitude. The longitude specifies the east-west position of a point on the Earth's surface.
        /// </summary>
        /// <value>The longitude.</value>
        double Longitude { get; }

        /// <summary>
        /// Gets the altitude. The altitude is the height of the location.
        /// </summary>
        /// <value>The altitude.</value>
        double Altitude { get; }
    }

}
