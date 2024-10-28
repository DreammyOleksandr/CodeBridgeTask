using Microsoft.EntityFrameworkCore;
using CodeBridgeTask.DataAccess;
using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DataAccess.Repositories.DogRepository;

public class DogRepositoryTests
{
    private readonly DogRepository _dogRepository;
    private readonly ApplicationDbContext _dbContext;

    public DogRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        
        _dbContext.Dogs.AddRange(new List<Dog>
        {
            new() { Id = 1, Name = "Buddy", Color = "Brown", TailLength = 5, Weight = 20 },
            new() { Id = 2, Name = "Max", Color = "Black", TailLength = 3, Weight = 25 },
            new() { Id = 3, Name = "Charlie", Color = "White", TailLength = 4, Weight = 15 },
        });
        _dbContext.SaveChanges();

        _dogRepository = new DogRepository(_dbContext);
    }

    [Fact]
    public async Task GetByNameAndColorAsync_ReturnsDog_WhenExists()
    {
        // Act
        var result = await _dogRepository.GetByNameAndColorAsync("Buddy", "Brown");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal("Buddy", result.Name);
    }

    [Fact]
    public async Task GetByNameAndColorAsync_ReturnsNull_WhenNotExists()
    {
        //Act
        var result = await _dogRepository.GetByNameAndColorAsync("NonExistent", "Green");
        
        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetRangeAsync_ReturnsDogs_WhenCalled()
    {
        // Arrange
        var queryParams = new QueryParams { PageNumber = 1, PageSize = 2 };

        // Act
        var result = await _dogRepository.GetRangeAsync(queryParams);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, dog => dog.Id == 1);
        Assert.Contains(result, dog => dog.Id == 2);
    }

    [Fact]
    public async Task GetRangeAsync_ReturnsEmpty_WhenNoDogsAvailable()
    {
        // Arrange
        var emptyDbContext = new ApplicationDbContext(
            new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "EmptyDatabase")
                .Options);

        var emptyDogRepository = new DogRepository(emptyDbContext);
        var queryParams = new QueryParams { PageNumber = 1, PageSize = 2 };

        // Act
        var result = await emptyDogRepository.GetRangeAsync(queryParams);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task IsExistingAsync_ReturnsTrue_WhenDogExists()
    {
        // Arrange
        var dog = new Dog { Id = 1, Name = "Buddy", Color = "Brown", TailLength = 5, Weight = 20 };

        // Act
        var result = await _dogRepository.IsExistingAsync(dog);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsExistingAsync_ReturnsFalse_WhenDogDoesNotExist()
    {
        // Arrange
        var dog = new Dog { Id = 0, Name = "NonExistent", Color = "Black", TailLength = 5, Weight = 20 };

        // Act
        var result = await _dogRepository.IsExistingAsync(dog);

        // Assert
        Assert.False(result);
    }
}
