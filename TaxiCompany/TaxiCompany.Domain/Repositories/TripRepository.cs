using Microsoft.EntityFrameworkCore;
using TaxiCompany.Domain.Context;
using TaxiCompany.Domain.Entities;

namespace TaxiCompany.Domain.Repositories;

/// <summary>
/// Класс <c>Репозиторий поездок</c>.
/// Содержит методы для получения, добавления, изменения и удаления поездки.
/// </summary>
public class TripRepository(TaxiCompanyDbContext context) : IRepository<Trip>
{
    /// <summary>
    /// Получает поездку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор поездки.</param>
    /// <returns>Поездка, если найдена; иначе null.</returns>
    public async Task<Trip?> GetAsync(int id) => await context.Trips.FirstOrDefaultAsync(t => t.Id == id);

    /// <summary>
    /// Получает все поездки.
    /// </summary>
    /// <returns>Коллекция всех поездок.</returns>
    public async Task<IEnumerable<Trip>> GetAsync() => await context.Trips.ToListAsync();

    /// <summary>
    /// Добавляет новую поездку.
    /// </summary>
    /// <param name="value">Объект поездки для добавления.</param>
    public async Task PostAsync(Trip value)
    {
        context.Trips.Add(value);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет данные поездки по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор обновляемой поездки.</param>
    /// <param name="value">Новые данные для поездки.</param>
    /// <returns>True, если обновление прошло успешно; иначе false.</returns>
    public async Task<bool> PutAsync(int id, Trip value)
    {
        var oldTrip = await GetAsync(id);

        if (oldTrip == null) return false;

        oldTrip.Departure = value.Departure;
        oldTrip.Destination = value.Destination;
        oldTrip.Date = value.Date;
        oldTrip.DrivingTime = value.DrivingTime;
        oldTrip.Cost = value.Cost;
        oldTrip.AssignedClientId = value.AssignedClientId;
        oldTrip.AssignedCarId = value.AssignedCarId;
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Удаляет поездку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой поездки.</param>
    /// <returns>True, если удаление прошло успешно; иначе false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var oldTrip = await GetAsync(id);

        if (oldTrip == null) return false;

        context.Trips.Remove(oldTrip);
        await context.SaveChangesAsync();

        return true;
    }
}
