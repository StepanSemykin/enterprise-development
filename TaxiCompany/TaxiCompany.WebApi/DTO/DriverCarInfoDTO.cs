namespace TaxiCompany.WebApi.DTO;

public class DriverCarInfoDTO
{
    /// <summary>
    /// Информация о водителе.
    /// </summary>
    public DriverDTO? Driver { get; set; }

    /// <summary>
    /// Информация об автомобиле, назначенном водителю.
    /// </summary>
    public CarDTO? Car { get; set; }
}
