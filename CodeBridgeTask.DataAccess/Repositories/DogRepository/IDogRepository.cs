using CodeBridgeTask.DataAccess.Models;

namespace CodeBridgeTask.DataAccess.Repositories.DogRepository;

public interface IDogRepository
{
    Task<IEnumerable<Dog>> GetAll();
    Task<Dog> Create(Dog dog);
}
