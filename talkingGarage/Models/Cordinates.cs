using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace talkingGarage.Models
{
    public class Cordinates
    {
        public List<string> nearestPlaces(List<double> data)
        {
            List<string> tempList = new List<string>();
            foreach (double d in data)
            {
                if (d > 0.00 && d < 1.50)
                {
                    tempList.Add(d.ToString("0.00"));
                }
            }
            return tempList;
        }
        public double getDistance(double lat1, double lon1, double lat2, double lon2)
        {
            if ((lat1 == lat2) && (lon1 == lon2))
            {
                return 0;
            }
            else
            {
                double theta = lon1 - lon2;
                double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                dist = Math.Acos(dist);
                dist = rad2deg(dist);
                dist = dist * 60 * 1.1515;
                dist = dist * 1.609344;
                return dist;
            }
        }
        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}