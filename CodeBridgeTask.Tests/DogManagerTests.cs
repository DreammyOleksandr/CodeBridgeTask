using CodeBridgeTask.BusinessLogic.Managers.DogManager;
using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DataAccess.Repositories.DogRepository;
using Moq;

namespace CodeBridgeTask.Tests;

public class DogManagerTests
{
    private readonly Mock<IDogRepository> _dogRepositoryMock;
    private readonly DogManager _dogManager;

    public DogManagerTests()
    {
        _dogRepositoryMock = new Mock<IDogRepository>();
        _dogManager = new DogManager(_dogRepositoryMock.Object);
    }

    [Fact]
    public async Task GetRangeAsync_ReturnsDogs_WhenDogsExist()
    {
        // Arrange
        var queryParams = new QueryParams { PageNumber = 1, PageSize = 2 };
        var dogs = new List<Dog>
        {
            new() { Id = 1, Name = "Buddy" },
            new() { Id = 2, Name = "Max" }
        };

        _dogRepositoryMock.Setup(repo => repo.GetRangeAsync(queryParams)).ReturnsAsync(dogs);

        // Act
        var result = await _dogManager.GetRangeAsync(queryParams);

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Equal(1, result.First().Id);
        Assert.Equal("Buddy", result.First().Name);
    }

    [Fact]
    public async Task GetRangeAsync_ThrowsArgumentException_WhenPageSizeIsInvalid()
    {
        // Arrange
        var queryParams = new QueryParams { PageNumber = 1, PageSize = 0 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _dogManager.GetRangeAsync(queryParams));
        Assert.Equal("Page size must be greater than 1.", exception.Message);
    }

    [Fact]
    public async Task GetRangeAsync_ThrowsArgumentException_WhenPageNumberIsInvalid()
    {
        // Arrange
        var queryParams = new QueryParams { PageNumber = 0, PageSize = 2 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _dogManager.GetRangeAsync(queryParams));
        Assert.Equal("Page number must be greater than 1.", exception.Message);
    }

    [Fact]
    public async Task CreateAsync_CreatesDog_WhenDogIsValid()
    {
        // Arrange
        var dog = new Dog { Id = 0, Name = "Bella", Color = "Black", TailLength = 5, Weight = 20 };
        _dogRepositoryMock.Setup(repo => repo.IsExistingAsync(dog)).ReturnsAsync(false);

        // Act
        await _dogManager.CreateAsync(dog);

        // Assert
        _dogRepositoryMock.Verify(repo => repo.CreateAsync(dog), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ThrowsArgumentException_WhenDogAlreadyExists()
    {
        // Arrange
        var dog = new Dog { Id = 0, Name = "Bella", Color = "Black", TailLength = 5, Weight = 20 };
        _dogRepositoryMock.Setup(repo => repo.IsExistingAsync(dog)).ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _dogManager.CreateAsync(dog));
        Assert.Equal("Dog already exists.", exception.Message);
    }

    [Fact]
    public async Task CreateAsync_ThrowsArgumentException_WhenWeightIsInvalid()
    {
        // Arrange
        var dog = new Dog { Id = 0, Name = "Charlie", Color = "Brown", TailLength = 5, Weight = 0 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _dogManager.CreateAsync(dog));
        Assert.Equal("Weight can't be less or equal 0.", exception.Message);
    }

    [Fact]
    public async Task CreateAsync_ThrowsArgumentException_WhenTailLengthIsInvalid()
    {
        // Arrange
        var dog = new Dog { Id = 0, Name = "Charlie", Color = "Brown", TailLength = -1, Weight = 20 };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => _dogManager.CreateAsync(dog));
        Assert.Equal("Tail length can't be less than 0.", exception.Message);
    }
}