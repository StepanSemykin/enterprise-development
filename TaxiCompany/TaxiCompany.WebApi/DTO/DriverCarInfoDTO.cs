namespace TaxiCompany.WebApi.Dto;

public class DriverCarInfoDto
{
    /// <summary>
    /// Информация о водителе.
    /// </summary>
    public DriverDto? Driver { get; set; }

    /// <summary>
    /// Информация об автомобиле, назначенном водителю.
    /// </summary>
    public CarDto? Car { get; set; }
}
