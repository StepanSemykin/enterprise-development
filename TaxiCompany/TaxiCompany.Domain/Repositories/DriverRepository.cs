using Microsoft.EntityFrameworkCore;
using TaxiCompany.Domain.Context;
using TaxiCompany.Domain.Entities;

namespace TaxiCompany.Domain.Repositories;

/// <summary>
/// Класс <c>Репозиторий водителей</c>.
/// Содержит методы для получения, добавления, изменения и удаления водителей.
/// </summary>
public class DriverRepository(TaxiCompanyDbContext context) : IRepository<Driver>
{
    /// <summary>
    /// Получает водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор водителя.</param>
    /// <returns>Водитель, если найден; иначе null.</returns>
    public async Task<Driver?> GetAsync(int id) => await context.Drivers.FirstOrDefaultAsync(d => d.Id == id);

    /// <summary>
    /// Получает всех водителей.
    /// </summary>
    /// <returns>Коллекция всех водителей.</returns>
    public async Task<IEnumerable<Driver>> GetAsync() => await context.Drivers.ToListAsync();

    /// <summary>
    /// Добавляет нового водителя.
    /// </summary>
    /// <param name="value">Объект водителя для добавления.</param>
    public async Task PostAsync(Driver value)
    {
        context.Drivers.Add(value);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновляет данные водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого водителя.</param>
    /// <param name="value">Новые данные для водителя.</param>
    /// <returns>True, если обновление прошло успешно; иначе false.</returns>
    public async Task<bool> PutAsync(int id, Driver value)
    {
        var oldDriver = await GetAsync(id);

        if (oldDriver == null) return false;

        oldDriver.FullName = value.FullName;
        oldDriver.PhoneNumber = value.PhoneNumber;
        oldDriver.Passport = value.Passport;
        oldDriver.Address = value.Address;
        oldDriver.AssignedCarId = value.AssignedCarId;
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Удаляет водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого водителя.</param>
    /// <returns>True, если удаление прошло успешно; иначе false.</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var oldDriver = await GetAsync(id);

        if (oldDriver == null) return false;

        context.Drivers.Remove(oldDriver);
        await context.SaveChangesAsync();

        return true;
    }
}
