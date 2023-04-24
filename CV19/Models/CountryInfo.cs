using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models;

internal class CountryInfo : PlaceInfo 
{
    private Point _Location;

    public override Point Location 
    {
        get
        {
            if (Provinces is null) return default;

            var average_x = Provinces.Average(p => p.Location.X);
            var average_y = Provinces.Average(p => p.Location.Y);

            return _Location = new Point(average_x, average_y);
        }
        set => _Location = value; 
    }

    public IEnumerable<PlaceInfo> Provinces { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Location.X}, {Location.Y})";
    }


    private IEnumerable<ConfirmedCount> _counts;
    public override IEnumerable<ConfirmedCount> Counts
    {
        get
        {
            if (_counts != null) return _counts;
            var pointsCount = Provinces.FirstOrDefault()?.Counts?.Count() ?? 0;
            if (pointsCount == 0) return Enumerable.Empty<ConfirmedCount>();

            var provincePoints = Provinces.Select(p => p.Counts.ToArray()).ToArray();

            var points = new ConfirmedCount[pointsCount];
            foreach (var province in provincePoints)
            {
                for (var i = 0; i < pointsCount; i++)
                {
                    if (points[i].Date == default)
                        points[i] = province[i];
                    else
                        points[i].Count += province[i].Count;
                }
            }
            return _counts = points;
        }
        set
        {
            _counts = value;
        }
    }
}

