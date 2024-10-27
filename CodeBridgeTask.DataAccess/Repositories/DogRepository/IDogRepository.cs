using CodeBridgeTask.DataAccess.Models;

namespace CodeBridgeTask.DataAccess.Repositories.DogRepository;

public interface IDogRepository
{
    Task<IEnumerable<Dog>> GetRange(QueryParams queryParams);
    Task<Dog> Create(Dog dog);
}
