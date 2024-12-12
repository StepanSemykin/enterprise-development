using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiCompany.Domain.Entities;

/// <summary>
/// Класс <c>Авто</c> хранит информацию о характеристиках автомобиля
/// </summary>
[Table("car")]
public class Car
{
    /// <summary>
    /// Идентификатор авто
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Цвет
    /// </summary>
    [Column("colour")]
    [MaxLength(30)]
    [Required]
    public required string Colour { get; set; }

    /// <summary>
    /// Модель
    /// </summary>
    [Column("model")]
    [MaxLength(50)]
    [Required]
    public required string Model { get; set; }

    /// <summary>
    /// Серийный номер
    /// </summary>
    [Column("serial_number")]
    [MaxLength(10)]
    [Required]
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Год выпуска
    /// </summary>
    [Column("release_year")]
    [Required]
    public required DateTime ReleaseYear { get; set; }

    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    [Column("assigned_driver_id")]
    [Required]
    public required int AssignedDriverId { get; set; }
}