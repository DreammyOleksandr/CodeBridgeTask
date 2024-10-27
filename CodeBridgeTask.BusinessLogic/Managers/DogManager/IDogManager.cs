using CodeBridgeTask.DataAccess.Models;

namespace CodeBridgeTask.BusinessLogic.Managers.DogManager;

public interface IDogManager
{
    Task<IEnumerable<Dog>> GetAll();
    Task<Dog> Create(Dog dog);
}