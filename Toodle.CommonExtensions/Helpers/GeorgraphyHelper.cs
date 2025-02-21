
using System;

namespace Toodle.CommonExtensions.Helpers
{
    /// <summary>
    /// Provides extension methods for working with Geographic coordinates.
    /// </summary>
    public static class GeographyHelper
    {
        /// <summary>
        /// Calculates the distance in meters between two geographic coordinates.
        /// </summary>
        /// <param name="lat1">The latitude of the first point in decimal degrees.</param>
        /// <param name="lon1">The longitude of the first point in decimal degrees.</param>
        /// <param name="lat2">The latitude of the second point in decimal degrees.</param>
        /// <param name="lon2">The longitude of the second point in decimal degrees.</param>
        /// <returns>The distance in meters between the two points using the Haversine formula.</returns>
        /// <example>
        /// <code>
        /// double distance = GeographyHelper.GetDistance(51.5074, -0.1278, 48.8566, 2.3522); // London to Paris
        /// </code>
        /// </example>
        /// <remarks>
        /// Uses the Haversine formula to calculate great-circle distances between two points on a sphere.
        /// Earth radius used is 6376500 meters.
        /// </remarks>
        public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var d1 = lat1 * (Math.PI / 180.0);
            var num1 = lon1 * (Math.PI / 180.0);
            var d2 = lat2 * (Math.PI / 180.0);
            var num2 = lon2 * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
