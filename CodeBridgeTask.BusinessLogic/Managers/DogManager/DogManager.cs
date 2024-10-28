using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DataAccess.Repositories.DogRepository;

namespace CodeBridgeTask.BusinessLogic.Managers.DogManager;

public class DogManager(IDogRepository dogRepository) : IDogManager
{
    private readonly IDogRepository _dogRepository = dogRepository;

    public async Task<IEnumerable<Dog>> GetRangeAsync(QueryParams queryParams)
    {
        if (queryParams.PageSize < 1) throw new ArgumentException("Page size must be greater than 1.");
        if (queryParams.PageNumber < 1) throw new ArgumentException("Page number must be greater than 1.");
        return await _dogRepository.GetRangeAsync(queryParams);
    }

    public async Task CreateAsync(Dog dog)
    {
        if (dog.Weight <= 0) throw new ArgumentException("Weight can't be less or equal 0.");
        if (dog.TailLength < 0) throw new ArgumentException("Tail length can't be less than 0.");
        if (await _dogRepository.IsExistingAsync(dog)) throw new ArgumentException("Dog already exists.");
        if (dog.Id != 0) throw new ArgumentException("Dog Id should be 0 on creation.");

        _dogRepository.CreateAsync(dog);
    }
}