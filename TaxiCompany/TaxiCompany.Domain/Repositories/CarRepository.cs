using Microsoft.EntityFrameworkCore;
using TaxiCompany.Domain.Context;
using TaxiCompany.Domain.Entities;

namespace TaxiCompany.Domain.Repositories;

/// <summary>
/// Класс <c>Репозиторий авто</c>.
/// Содержит методы для получения, добавления, изменения и удаления авто.
/// </summary>
public class CarRepository(TaxiCompanyDbContext context) : IRepository<Car>
{
    /// <summary>
    /// Получает автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <returns>Объект автомобиля, если найден; иначе null.</returns>
    public async Task<Car?> GetAsync(int id) => await context.Cars.FirstOrDefaultAsync(c => c.Id == id);

    /// <summary>
    /// Получает все автомобили.
    /// </summary>
    /// <returns>Коллекция всех авто.</returns>
    public async Task<IEnumerable<Car>> GetAsync() => await context.Cars.ToListAsync();

    /// <summary>
    /// Добавляет новый автомобиль.
    /// </summary>
    /// <param name="value">Объект автомобиля для добавления.</param>
    public async Task PostAsync(Car value)
    {
        context.Cars.Add(value);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет данные автомобиля по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор авто.</param>
    /// <param name="value">Новые данные для авто.</param>
    /// <returns>True, если обновление прошло успешно; иначе false.</returns>
    public async Task<bool> PutAsync(int id, Car value)
    {
        var oldCar = await GetAsync(id);

        if (oldCar == null) return false;

        oldCar.Colour = value.Colour;
        oldCar.Model = value.Model;
        oldCar.SerialNumber = value.SerialNumber;
        oldCar.RealeseYear = value.RealeseYear;
        oldCar.AssignedDriverId = value.AssignedDriverId;
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Удаляет автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого автомобиля.</param>
    /// <returns>True, если удаление прошло успешно; иначе false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var oldCar = await GetAsync(id);

        if (oldCar == null) return false;

        context.Cars.Remove(oldCar);
        await context.SaveChangesAsync();

        return true;
    }
}
