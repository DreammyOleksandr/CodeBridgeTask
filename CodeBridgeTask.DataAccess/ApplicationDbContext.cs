using CodeBridgeTask.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBridgeTask.DataAccess;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Dog> Dogs { get; set; }
}