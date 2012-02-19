// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusDetail.cs" company="Arosbi">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the BusItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Arosbi.DnDZgZ.UI.Model
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Linq;

    /// <summary>
    /// Defines the details of a bus stop.
    /// </summary>
    public class BusDetail : DetailBase
    {
        public class BusArrival
        {
            public BusArrival(string[] data)
            {
                var index = data[0].IndexOf(']');

                this.BusNumber = data[0].Substring(1, index - 1);
                this.TimeToArrive = data[0].Substring(index + 2);

                var match = Regex.Match(this.TimeToArrive, @"\d+");
                this.MinutesToArrive = match.Success ? int.Parse(match.Captures[0].Value) : default(int?);

                this.Direction = data[1];
            }

            public string BusNumber { get; private set; }
            public int? MinutesToArrive { get; private set; }
            public string TimeToArrive { get; private set; }
            public string Direction { get; private set; }

            public override string ToString()
            {
                return string.Format("{0}: {1}", this.BusNumber, this.TimeToArrive);
            }
        }

        private BusArrival[] timelines;

        public IEnumerable<BusArrival> Timelines
        {
            get
            {
                if (this.timelines == null)
                {
                    this.timelines = new BusArrival[this.Items.Length];

                    for (var i = 0; i < this.Items.Length; i++)
                    {
                        this.timelines[i] = new BusArrival(this.Items[i]);
                    }

                    this.timelines = this.timelines
                        .OrderBy(t => !t.MinutesToArrive.HasValue)
                        .ThenBy(t => t.MinutesToArrive)
                        .ToArray();
                }

                return this.timelines;
            }
        }
    }
}