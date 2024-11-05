namespace TaxiCompany.WebApi.Dto;

public class ClientDto
{
    /// <summary>
    /// Имя и фамилия
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}
