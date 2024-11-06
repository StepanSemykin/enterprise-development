namespace TaxiCompany.WebApi.Dto;

public class DriverTripCountDto
{
    /// <summary>
    /// Информация о водителе.
    /// </summary>
    public DriverDto? Driver { get; set; }

    /// <summary>
    /// Общее количество поездок, совершенных водителем.
    /// </summary>
    public int TripCount { get; set; }
}
