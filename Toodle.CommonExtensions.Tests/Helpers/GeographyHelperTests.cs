using System;
using Toodle.CommonExtensions.Helpers;
using Xunit;

public class GeographyHelperTests
{
    private const double Epsilon = 0.1; // Tolerance for floating-point comparison (in meters)

    [Fact]
    public void GetDistance_SamePoint_ReturnsZero()
    {
        // Arrange
        double lat = 40.7128;
        double lon = -74.0060;

        // Act
        double distance = GeographyHelper.GetDistance(lat, lon, lat, lon);

        // Assert
        Assert.Equal(0, distance, Epsilon);
    }

    [Fact]
    public void GetDistance_KnownDistance_ReturnsCorrectValue()
    {
        // Arrange - London to Paris coordinates
        double londonLat = 51.5074;
        double londonLon = -0.1278;
        double parisLat = 48.8566;
        double parisLon = 2.3522;
        double expectedDistance = 343500; // ~343 km in meters with some tolerance

        // Act
        double distance = GeographyHelper.GetDistance(londonLat, londonLon, parisLat, parisLon);

        // Assert
        Assert.InRange(distance, expectedDistance - 1000, expectedDistance + 1000);
    }

    [Fact]
    public void GetDistance_AntipodePoints_ReturnsHalfEarthCircumference()
    {
        // Arrange
        double lat1 = 0;
        double lon1 = 0;
        double lat2 = 0;
        double lon2 = 180;
        double expectedDistance = Math.PI * 6371000; // Half of Earth's circumference in meters

        // Act
        double distance = GeographyHelper.GetDistance(lat1, lon1, lat2, lon2);

        // Assert
        Assert.InRange(distance, expectedDistance - 1000, expectedDistance + 1000);
    }

    [Fact]
    public void GetDistance_PolesToEquator_ReturnsQuarterEarthCircumference()
    {
        // Arrange
        double northPoleLat = 90;
        double northPoleLon = 0; // Longitude is arbitrary at the poles
        double equatorLat = 0;
        double equatorLon = 0;
        double expectedDistance = Math.PI * 6371000 / 2; // Quarter of Earth's circumference in meters

        // Act
        double distance = GeographyHelper.GetDistance(northPoleLat, northPoleLon, equatorLat, equatorLon);

        // Assert
        Assert.InRange(distance, expectedDistance - 1000, expectedDistance + 1000);
    }

    [Fact]
    public void GetDistance_CrossingDateLine_ReturnsCorrectValue()
    {
        // Arrange
        double tokyoLat = 35.6762;
        double tokyoLon = 139.6503;
        double losAngelesLat = 34.0522;
        double losAngelesLon = -118.2437;
        double expectedDistance = 8800000; // ~8,800 km in meters

        // Act
        double distance = GeographyHelper.GetDistance(tokyoLat, tokyoLon, losAngelesLat, losAngelesLon);

        // Assert
        Assert.InRange(distance, expectedDistance - 100000, expectedDistance + 100000);
    }

    [Fact]
    public void GetDistance_Symmetry_ReturnsEqualDistances()
    {
        // Arrange
        double sydneyLat = -33.8688;
        double sydneyLon = 151.2093;
        double rioLat = -22.9068;
        double rioLon = -43.1729;

        // Act
        double distanceAtoB = GeographyHelper.GetDistance(sydneyLat, sydneyLon, rioLat, rioLon);
        double distanceBtoA = GeographyHelper.GetDistance(rioLat, rioLon, sydneyLat, sydneyLon);

        // Assert
        Assert.Equal(distanceAtoB, distanceBtoA, Epsilon);
    }

    [Fact]
    public void GetDistance_InvalidCoordinates_HandlesExtremeLat()
    {
        // Arrange
        double invalidLat = 100; // Beyond 90 degrees
        double validLon = 0;
        double validLat = 0;

        // Act & Assert
        // This test verifies the method handles latitude values outside the valid range (-90 to 90)
        // The actual behavior might vary based on implementation - might return a specific value or throw
        var distance = GeographyHelper.GetDistance(invalidLat, validLon, validLat, validLon);

        // The Haversine formula will still produce a result, but it won't be geographically meaningful
        Assert.True(distance >= 0); // At minimum, verify we don't get negative distances
    }

    [Fact]
    public void GetDistance_ShortDistance_HighPrecision()
    {
        // Arrange - Two points 100 meters apart at the equator
        // At the equator, 0.001 degree of longitude is approximately 111 meters
        double lat1 = 0;
        double lon1 = 0;
        double lat2 = 0;
        double lon2 = 0.0009; // ~100 meters at the equator
        double expectedDistance = 100;

        // Act
        double distance = GeographyHelper.GetDistance(lat1, lon1, lat2, lon2);

        // Assert
        Assert.InRange(distance, expectedDistance - 1, expectedDistance + 1);
    }

    [Theory]
    [InlineData(40.7128, -74.0060, 34.0522, -118.2437, 3935000)] // New York to Los Angeles ~3,935 km
    [InlineData(55.7558, 37.6173, 31.2304, 121.4737, 7150000)]   // Moscow to Shanghai ~7,150 km
    [InlineData(1.3521, 103.8198, 1.3521, 103.8298, 1100)]       // Singapore, 1km east
    public void GetDistance_VariousPoints_ReturnsExpectedDistances(
        double lat1, double lon1, double lat2, double lon2, double expectedMeters)
    {
        // Act
        double distance = GeographyHelper.GetDistance(lat1, lon1, lat2, lon2);

        // Assert - Allow 5% margin for different Earth models and rounding
        double tolerance = expectedMeters * 0.05;
        Assert.InRange(distance, expectedMeters - tolerance, expectedMeters + tolerance);
    }
}