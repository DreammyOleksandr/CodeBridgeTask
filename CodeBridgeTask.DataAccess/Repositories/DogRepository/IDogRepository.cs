using CodeBridgeTask.DataAccess.Models;

namespace CodeBridgeTask.DataAccess.Repositories.DogRepository;

public interface IDogRepository
{
    Task<Dog> GetByNameAndColorAsync(string name, string color);
    Task<IEnumerable<Dog>> GetRange(QueryParams queryParams);
    Task Create(Dog dog);
    Task<bool> IsExistingAsync(Dog dog);
}
