﻿namespace Arosbi.DnDZgZ.UI.Model
{
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;
    using System.Linq;

    public class BusStopDetail : DetailBase
    {
        public class BusSchedule
        {
            public BusSchedule(string[] data)
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

            public string ArrivalInfo
            {
                get
                {
                    return this.ToString();
                }
            }

            public override string ToString()
            {
                return string.Format("{0}: {1}", this.BusNumber, this.TimeToArrive);
            }
        }

        private bool timelinesLoaded = false;

        private readonly ObservableCollection<BusSchedule> timelines = new ObservableCollection<BusSchedule>();

        public ObservableCollection<BusSchedule> Timelines
        {
            get
            {
                if (!this.timelinesLoaded)
                {
                    this.timelinesLoaded = true;

                    var llegadas = this.Items
                        .Select(data => new BusSchedule(data));

                    var llegadasOrdenadas = llegadas
                        .OrderBy(t => !t.MinutesToArrive.HasValue)
                        .ThenBy(t => t.MinutesToArrive);

                    foreach (var busArrival in llegadasOrdenadas)
                    {
                        this.timelines.Add(busArrival);
                    }
                }

                return this.timelines;
            }
        }
    }
}