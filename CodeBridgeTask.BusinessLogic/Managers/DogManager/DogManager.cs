using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DataAccess.Repositories.DogRepository;
using Microsoft.EntityFrameworkCore;

namespace CodeBridgeTask.BusinessLogic.Managers.DogManager;

public class DogManager(IDogRepository dogRepository) : IDogManager
{
    private readonly IDogRepository _dogRepository = dogRepository;

    public async Task<IEnumerable<Dog>> GetRange(QueryParams queryParams)
        => await _dogRepository.GetRange(queryParams);
    
    public Task<Dog> Create(Dog dog)
    {
        throw new NotImplementedException();
    }
}