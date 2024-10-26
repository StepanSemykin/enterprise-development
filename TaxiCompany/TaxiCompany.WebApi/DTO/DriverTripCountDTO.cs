namespace TaxiCompany.WebApi.DTO;

public class DriverTripCountDTO
{
    /// <summary>
    /// Информация о водителе.
    /// </summary>
    public DriverDTO? Driver { get; set; }

    /// <summary>
    /// Общее количество поездок, совершенных водителем.
    /// </summary>
    public int TripCount { get; set; }
}
