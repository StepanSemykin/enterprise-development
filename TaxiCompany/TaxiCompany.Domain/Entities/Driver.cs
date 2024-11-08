using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiCompany.Domain.Entities;

/// <summary>
/// Класс <c>Водитель</c> хранит информацию о водителе
/// </summary>
[Table("driver")]
public class Driver
{
    /// <summary>
    /// Идентификатор водителя
    /// </summary>
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Имя и фамилия
    /// </summary>
    [Column("full_name")]
    [MaxLength(50)]
    [Required]
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Column("phone_number")]
    [MaxLength(11)]
    [Required]
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Паспортные данные
    /// </summary>
    [Column("passport")]
    [MaxLength(10)]
    [Required]
    public required string Passport { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    [Column("address")]
    [MaxLength(80)]
    [Required]
    public required string Address { get; set; }

    /// <summary>
    /// Идентификатор закрепленного автомобиля
    /// </summary>
    [Column("assigned_car_id")]
    [Required]
    public required int AssignedCarId { get; set; }
}