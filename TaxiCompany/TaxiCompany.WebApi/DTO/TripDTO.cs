namespace TaxiCompany.WebApi.Dto;

public class TripDto
{
    /// <summary>
    /// Пункт отправления
    /// </summary>
    public required string Departure { get; set; }

    /// <summary>
    /// Пункт назначения
    /// </summary>
    public required string Destination { get; set; }

    /// <summary>
    /// Дата поездки
    /// </summary>
    public required DateTime Date { get; set; }

    /// <summary>
    /// Время в движении
    /// </summary>
    public TimeOnly DrivingTime { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    public required decimal Cost { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int AssignedClientId { get; set; }

    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public required int AssignedCarId { get; set; }
}
