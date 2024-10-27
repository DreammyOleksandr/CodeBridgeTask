using CodeBridgeTask.DataAccess.Models;

namespace CodeBridgeTask.BusinessLogic.Managers.DogManager;

public interface IDogManager
{
    Task<IEnumerable<Dog>> GetRange(QueryParams queryParams);
    Task<Dog> Create(Dog dog);
}