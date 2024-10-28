using System.Linq.Expressions;
using System.Reflection;
using CodeBridgeTask.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBridgeTask.DataAccess.Repositories.DogRepository;

public class DogRepository(ApplicationDbContext db) : IDogRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<Dog> GetByNameAndColorAsync(string name, string color)
        => await _db.Dogs.FirstOrDefaultAsync(d => d.Name == name && d.Color == color);

    public async Task<IEnumerable<Dog>> GetRange(QueryParams queryParams)
    {
        var query = _db.Dogs.AsQueryable();

        Expression<Func<Dog, object>>? orderExpression = queryParams.Attribute switch
        {
            "name" => d => d.Name,
            "color" => d => d.Color,
            "tail_length" => d => d.TailLength,
            "weight" => d => d.Weight,
            _ => null
        };

        if (orderExpression is not null)
            query = queryParams.Order == "desc"
                ? query.OrderByDescending(orderExpression)
                : query.OrderBy(orderExpression);

        //Default page values are: Number: 1; Size: 10
        query = query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize);

        return await query.ToListAsync();
    }


    public async Task Create(Dog dog)
    {
        await _db.Dogs.AddAsync(dog);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> IsExistingAsync(Dog dog)
    {
        Dog existingDog = await GetByNameAndColorAsync(dog.Name, dog.Color);
        return existingDog is not null;
    }
}