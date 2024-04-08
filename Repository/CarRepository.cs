using CarShops.Data;
using CarShops.Interfaces;
using CarShops.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShops.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly DBContext _context;
        public CarRepository(DBContext context)
        {
            _context = context;
        }
        public bool Add(Car car)
        {
            _context.Add(car);
            return Save();
        }

        public bool Delete(Car car)
        {
            _context.Remove(car);
            return Save();
        }

        public async Task<IEnumerable<Car>> GetALL()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Car> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Cars.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Car>> GetCarAvailable(bool carType)
        {
            return await _context.Cars.Where(c => c.Available).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Car car)
        {
            _context.Update(car);
            return Save();
        }
    }
}
