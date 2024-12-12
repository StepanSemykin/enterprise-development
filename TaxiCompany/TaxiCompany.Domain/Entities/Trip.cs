using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiCompany.Domain.Entities;

/// <summary>
/// Класс <c>Поездка</c> хранит информацию о поездке
/// </summary>
[Table("trip")]
public class Trip
{
    /// <summary>
    /// Идентификатор поездки
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Пункт отправления
    /// </summary>
    [Column("departure")]
    [MaxLength(100)]
    [Required]
    public required string Departure { get; set; }

    /// <summary>
    /// Пункт назначения
    /// </summary>
    [Column("destination")]
    [MaxLength(100)]
    [Required]
    public required string Destination { get; set; }

    /// <summary>
    /// Дата поездки
    /// </summary>
    [Column("date")]
    [Required]
    public required DateTime Date { get; set; }

    /// <summary>
    /// Время в движении
    /// </summary>
    [Column("driving_time")]
    [Required]
    public TimeOnly DrivingTime { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    [Column("cost")]
    [Required]
    public required decimal Cost { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Column("assigned_client_id")]
    [Required]
    public required int AssignedClientId { get; set; }

    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    [Column("assigned_car_id")]
    [Required]
    public required int AssignedCarId { get; set; }
}