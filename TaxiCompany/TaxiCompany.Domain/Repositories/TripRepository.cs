namespace TaxiCompany.Domain.Repositories;

/// <summary>
/// Класс <c>Репозиторий поездок</c>.
/// Содержит методы для получения, добавления, изменения и удаления поездки.
/// </summary>
public class TripRepository : IRepository<Trip>
{
    private readonly List<Trip> _trips = [];

    /// <summary>
    /// Получает поездку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор поездки.</param>
    /// <returns>Поездка, если найдена; иначе null.</returns>
    public Trip? Get(int id) => _trips.FirstOrDefault(t => t.Id == id);

    /// <summary>
    /// Получает все поездки.
    /// </summary>
    /// <returns>Коллекция всех поездок.</returns>
    public IEnumerable<Trip> Get() => _trips;

    /// <summary>
    /// Добавляет новую поездку.
    /// </summary>
    /// <param name="value">Объект поездки для добавления.</param>
    public void Post(Trip value)
    {
       _trips.Add(value);
    }

    /// <summary>
    /// Обновляет данные поездки по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор обновляемой поездки.</param>
    /// <param name="value">Новые данные для поездки.</param>
    /// <returns>True, если обновление прошло успешно; иначе false.</returns>
    public bool Put(int id, Trip value)
    {
        var oldTrip = Get(id);

        if (oldTrip == null) return false;

        oldTrip.Departure = value.Departure;
        oldTrip.Destination = value.Destination;
        oldTrip.Date = value.Date;
        oldTrip.DrivingTime = value.DrivingTime;    
        oldTrip.Cost = value.Cost;  
        oldTrip.AssignedClientId = value.AssignedClientId;
        oldTrip.AssignedCarId = value.AssignedCarId;

        return true;
    }

    /// <summary>
    /// Удаляет поездку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой поездки.</param>
    /// <returns>True, если удаление прошло успешно; иначе false.</returns>
    public bool Delete(int id)
    {
        var oldTrip = Get(id);

        if (oldTrip == null) return false;

        _trips.Remove(oldTrip); 

        return true;
    }
}
