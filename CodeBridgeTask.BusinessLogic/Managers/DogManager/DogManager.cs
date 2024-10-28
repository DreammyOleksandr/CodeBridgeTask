using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DataAccess.Repositories.DogRepository;

namespace CodeBridgeTask.BusinessLogic.Managers.DogManager;

public class DogManager(IDogRepository dogRepository) : IDogManager
{
    private readonly IDogRepository _dogRepository = dogRepository;

    public async Task<IEnumerable<Dog>> GetRange(QueryParams queryParams)
        => await _dogRepository.GetRange(queryParams);

    public async Task Create(Dog dog)
    {
        if (dog.Weight <= 0) throw new ArgumentException("Weight can't be less or equal 0");
        if (dog.TailLength < 0) throw new ArgumentException("Tail length can't be less than 0");
        if (await _dogRepository.IsExistingAsync(dog)) throw new ArgumentException("Dog already exists");
        if (dog.Id != 0) throw new ArgumentException("Dog Id should be 0 on creation");

        _dogRepository.Create(dog);
    }
}