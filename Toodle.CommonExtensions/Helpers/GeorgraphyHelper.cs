
using System;

namespace Toodle.CommonExtensions.Helpers
{
    /// <summary>
    /// Provides extension methods for working with Geographic coordinates.
    /// </summary>
    public static class GeographyHelper
    {
        /// <summary>
        /// Earth's mean radius in meters.
        /// </summary>
        private const double EarthRadiusMeters = 6371000.0;

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
        /// Earth radius used is 6371000 meters (mean radius).
        /// </remarks>
        public static double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            // Convert degrees to radians
            double lat1Rad = ToRadians(lat1);
            double lon1Rad = ToRadians(lon1);
            double lat2Rad = ToRadians(lat2);
            double lon2Rad = ToRadians(lon2);

            // Calculate differences
            double deltaLat = lat2Rad - lat1Rad;
            double deltaLon = lon2Rad - lon1Rad;

            // Apply Haversine formula
            double a = Math.Sin(deltaLat / 2.0) * Math.Sin(deltaLat / 2.0) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLon / 2.0) * Math.Sin(deltaLon / 2.0);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1.0 - a));

            return EarthRadiusMeters * c;
        }

        /// <summary>
        /// Converts degrees to radians.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in radians.</returns>
        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}