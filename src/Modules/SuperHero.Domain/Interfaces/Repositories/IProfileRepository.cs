using SuperHero.Domain.Entities;
using System.Threading.Tasks;

namespace SuperHero.Domain.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        Task<Profile> GetByIdAsync(int id); 
    }
}
