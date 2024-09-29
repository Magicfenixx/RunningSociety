using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<IEnumerable<Race>> GetAllRacesByCity(string city);
        bool Add(Race Race);
        bool Update(Race Race);
        bool Delete(Race Race);
        bool Save();
    }
}
