namespace TaxiCompany.Domain;
/// <summary>
/// Класс <c>Авто</c> хранит информацию о характеристиках автомобиля
/// </summary>
public class Car
{
    /// <summary>
    /// Идентификатор авто
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Цвет
    /// </summary>
    public required string Colour { get; set; }
    /// <summary>
    /// Модель
    /// </summary>
    public required string Model { get; set; }
    /// <summary>
    /// Серийный номер
    /// </summary>
    public required string SerialNumber { get; set; }
    /// <summary>
    /// Год выпуска
    /// </summary>
    public required DateTime RealeseYear { get; set; }
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    public required int AssignedDriverId { get; set; }
}
