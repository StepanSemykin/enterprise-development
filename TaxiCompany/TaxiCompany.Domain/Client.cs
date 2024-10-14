namespace TaxiCompany.Domain;
/// <summary>
/// Класс <c>Клиент</c> хранит информацию о клиенте
/// </summary>
public class Client
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Имя и фамилия
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}
