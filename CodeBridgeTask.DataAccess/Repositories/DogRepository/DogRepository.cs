using CodeBridgeTask.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBridgeTask.DataAccess.Repositories.DogRepository;

public class DogRepository(ApplicationDbContext db) : IDogRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<IEnumerable<Dog>> GetAll() => await _db.Dogs.ToListAsync();
    

    public Task<Dog> Create(Dog dog)
    {
        throw new NotImplementedException();
    }
}