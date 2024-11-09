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
    public Driver? Get(int id) => context.Drivers.FirstOrDefault(d => d.Id == id);

    /// <summary>
    /// Получает всех водителей.
    /// </summary>
    /// <returns>Коллекция всех водителей.</returns>
    public IEnumerable<Driver> Get() => context.Drivers;

    /// <summary>
    /// Добавляет нового водителя.
    /// </summary>
    /// <param name="value">Объект водителя для добавления.</param>
    public void Post(Driver value)
    {
        context.Drivers.Add(value);
        context.SaveChanges();
    }

    /// <summary>
    /// Обновляет данные водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор обновляемого водителя.</param>
    /// <param name="value">Новые данные для водителя.</param>
    /// <returns>True, если обновление прошло успешно; иначе false.</returns>
    public bool Put(int id, Driver value)
    {
        var oldDriver = Get(id);
        
        if (oldDriver == null) return false;

        oldDriver.FullName = value.FullName;
        oldDriver.PhoneNumber = value.PhoneNumber;
        oldDriver.Passport = value.Passport;
        oldDriver.Address = value.Address;
        oldDriver.AssignedCarId = value.AssignedCarId;
        context.SaveChanges();

        return true;
    }

    /// <summary>
    /// Удаляет водителя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемого водителя.</param>
    /// <returns>True, если удаление прошло успешно; иначе false.</returns>
    public bool Delete(int id)
    {
        var oldDriver = Get(id);

        if (oldDriver == null) return false;

        context.Drivers.Remove(oldDriver);
        context.SaveChanges();

        return true;
    }


}
