using TaxiCompany.Domain.Entities;

namespace TaxiCompany.Domain.Repositories;

/// <summary>
/// Класс <c>Репозиторий авто</c>.
/// Содержит методы для получения, добавления, изменения и удаления авто.
/// </summary>
public class CarRepository : IRepository<Car>
{
    private readonly List<Car> _cars = [];
    private int _id = 1;

    /// <summary>
    /// Получает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Объект автомобиля, если найден; иначе null.</returns>
    public Car? Get(int id) => _cars.FirstOrDefault(c => c.Id == id);

    /// <summary>
    /// Получает все автомобили.
    /// </summary>
    /// <returns>Коллекция всех авто.</returns>
    public IEnumerable<Car> Get() => _cars;

    /// <summary>
    /// Добавляет новый автомобиль.
    /// </summary>
    /// <param name="value">Объект автомобиля для добавления.</param>
    public void Post(Car value)
    {
        value.Id = _id++;
        _cars.Add(value);
    }

    /// <summary>
    /// Обновляет данные автомобиля по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор авто.</param>
    /// <param name="value">Новые данные для авто.</param>
    /// <returns>True, если обновление прошло успешно; иначе false.</returns>
    public bool Put(int id, Car value)
    {
        var oldCar = Get(id);

        if (oldCar == null) return false;

        oldCar.Colour = value.Colour;   
        oldCar.Model = value.Model;
        oldCar.SerialNumber = value.SerialNumber;
        oldCar.RealeseYear = value.RealeseYear;
        oldCar.AssignedDriverId = value.AssignedDriverId;

        return true;
    }

    /// <summary>
    /// Удаляет автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого автомобиля.</param>
    /// <returns>True, если удаление прошло успешно; иначе false.</returns>
    public bool Delete(int id)
    {
        var oldCar = Get(id);

        if (oldCar == null) return false;

        _cars.Remove(oldCar);

        return true;
    }
}
