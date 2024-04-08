using CarShops.Models;

namespace CarShops.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetALL();
        Task<Car> GetByIdAsync(int id);
        Task<Car> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Car>> GetCarAvailable(bool carType);
        bool Add(Car car);
        bool Update(Car car);
        bool Delete(Car car);
        bool Save();
    }
}
