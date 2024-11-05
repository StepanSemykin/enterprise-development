namespace TaxiCompany.WebApi.Dto;

public class DriverTripStatsDto
{
    /// <summary>
    /// Информация о водителе.
    /// </summary>
    public DriverDto? Driver { get; set; }

    /// <summary>
    /// Общее количество поездок, совершенных водителем.
    /// </summary>
    public int TripCount { get; set; }

    /// <summary>
    /// Среднее время в движении для поездок водителя.
    /// </summary>
    public TimeOnly AverageDrivingTime { get; set; }

    /// <summary>
    /// Максимальное время в движении для одной из поездок водителя.
    /// </summary>
    public TimeOnly MaxDrivingTime { get; set; }
}
