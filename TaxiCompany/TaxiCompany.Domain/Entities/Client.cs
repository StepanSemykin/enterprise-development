using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaxiCompany.Domain.Entities;

/// <summary>
/// Класс <c>Клиент</c> хранит информацию о клиенте
/// </summary>
[Table("client")]
public class Client
{
    /// <summary>
    /// Идентификатор клиента
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
}