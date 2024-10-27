using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DataAccess.Repositories.DogRepository;

namespace CodeBridgeTask.BusinessLogic.Managers.DogManager;

public class DogManager(IDogRepository dogRepository) : IDogManager
{
    private readonly IDogRepository _dogRepository = dogRepository;
    
    public async Task<IEnumerable<Dog>> GetAll() => await _dogRepository.GetAll();

    public Task<Dog> Create(Dog dog)
    {
        throw new NotImplementedException();
    }
}