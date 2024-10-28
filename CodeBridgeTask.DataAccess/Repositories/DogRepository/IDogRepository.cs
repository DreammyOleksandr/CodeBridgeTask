using CodeBridgeTask.DataAccess.Models;

namespace CodeBridgeTask.DataAccess.Repositories.DogRepository;

public interface IDogRepository
{
    Task<Dog> GetByNameAndColorAsync(string name, string color);
    Task<IEnumerable<Dog>> GetRangeAsync(QueryParams queryParams);
    Task CreateAsync(Dog dog);
    Task<bool> IsExistingAsync(Dog dog);
}
