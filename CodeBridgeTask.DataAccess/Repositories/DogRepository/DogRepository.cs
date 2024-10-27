using System.Linq.Expressions;
using System.Reflection;
using CodeBridgeTask.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBridgeTask.DataAccess.Repositories.DogRepository;

public class DogRepository(ApplicationDbContext db) : IDogRepository
{
    private readonly ApplicationDbContext _db = db;

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
        
        query = query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize);

        return await query.ToListAsync();
    }


    public Task<Dog> Create(Dog dog)
    {
        throw new NotImplementedException();
    }
}