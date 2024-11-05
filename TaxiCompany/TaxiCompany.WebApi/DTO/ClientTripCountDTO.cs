namespace TaxiCompany.WebApi.Dto;

public class ClientTripCountDto
{
    /// <summary>
    /// Информация о клиенте.
    /// </summary>
    public ClientDto? Client { get; set; }

    /// <summary>
    /// Общее количество поездок, совершенных клиентом.
    /// </summary>
    public int TripCount { get; set; }
}
