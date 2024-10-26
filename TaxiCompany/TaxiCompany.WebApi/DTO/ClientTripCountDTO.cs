namespace TaxiCompany.WebApi.DTO;

public class ClientTripCountDTO
{
    /// <summary>
    /// Информация о клиенте.
    /// </summary>
    public ClientDTO? Client { get; set; }

    /// <summary>
    /// Общее количество поездок, совершенных клиентом.
    /// </summary>
    public int TripCount { get; set; }
}
