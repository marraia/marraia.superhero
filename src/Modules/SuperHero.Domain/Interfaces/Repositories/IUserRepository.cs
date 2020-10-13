using SuperHero.Domain.Entities;
using System.Threading.Tasks;

namespace SuperHero.Domain.Interfaces.Repositories
{
    public interface IUserRepository 
    {
        Task<int> InsertAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetByLoginAsync(string login);
        Task<User> GetByIdAsync(int id);
    }
}
