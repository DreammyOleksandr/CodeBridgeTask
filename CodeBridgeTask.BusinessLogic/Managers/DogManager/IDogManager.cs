using CodeBridgeTask.DataAccess.Models;

namespace CodeBridgeTask.BusinessLogic.Managers.DogManager;

public interface IDogManager
{
    Task<IEnumerable<Dog>> GetRangeAsync(QueryParams queryParams);
    Task CreateAsync(Dog dog);
}