using System.Runtime.InteropServices;
using AutoMapper;
using CodeBridgeTask.BusinessLogic.Managers.DogManager;
using CodeBridgeTask.Controllers;
using CodeBridgeTask.DataAccess.Models;
using CodeBridgeTask.DTOs;
using CodeBridgeTask.MappingProfiles;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CodeBridgeTask.Tests;

public class DogControllerTests
{
    private readonly Mock<IDogManager> _dogManagerMock;
    private readonly DogsController _controller;
    private readonly IMapper _mapper;

    private readonly List<Dog> mockDogs =
    [
        new() { Id = 1, Name = "Neo", Color = "Red & Amber", TailLength = 1, Weight = 1 },
        new() { Id = 2, Name = "Jessy", Color = "Black", TailLength = 2, Weight = 2 },
        new() { Id = 3, Name = "Nessy", Color = "White", TailLength = 3, Weight = 3 },
        new() { Id = 4, Name = "Messy", Color = "Green", TailLength = 4, Weight = 4 },
        new() { Id = 5, Name = "Lessy", Color = "Blue", TailLength = 5, Weight = 5 },
        new() { Id = 6, Name = "Pepsi", Color = "Yellow", TailLength = 6, Weight = 6 },
    ];
    
    public DogControllerTests()
    {
        _dogManagerMock = new Mock<IDogManager>();
        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });

        _mapper = mapperConfig.CreateMapper();
        _controller = new DogsController(_dogManagerMock.Object, _mapper);
    }


    [Fact]
    public async Task GetAllDogs_ReturnsOkResult_WithListOfTwoDogs()
    {
        // Arrange
        _dogManagerMock.Setup(manager => manager.GetRangeAsync(null).Result).Returns(mockDogs);

        // Act
        var result = await _controller.GetRange(null);
        IEnumerable<DogDTO> actual = (result.Result as OkObjectResult).Value as IEnumerable<DogDTO>;

        // Assert
        Assert.IsType<ActionResult<IEnumerable<DogDTO>>>(result);
        Assert.Equal(6, actual.Count());
    }

    [Fact]
    public async Task GetDogsPaginatedRange_ReturnsListOfTwoDogs_WithIds5And6()
    {
        QueryParamsDTO queryParamsDto = new QueryParamsDTO()
        {
            Attribute = string.Empty,
            Order = string.Empty,
            PageNumber = 3,
            PageSize = 2,
        };
        // Arrange
        
        _dogManagerMock
            .Setup(manager => manager.GetRangeAsync(It.IsAny<QueryParams>()))
            .ReturnsAsync(mockDogs.Skip(4).Take(2).ToList());

        // Act
        var result = await _controller.GetRange(queryParamsDto);
        var actual = (result.Result as OkObjectResult).Value as List<DogDTO>;

        // Assert
        Assert.Equal(2, actual.Count());
        Assert.Equal(5, actual[0].Id);
        Assert.Equal(6, actual[1].Id);
    }
    
    [Fact]
    public async Task GetDogsRange_ReturnsFilteredDogs_ByName()
    {
        // Arrange
        QueryParamsDTO queryParamsDto = new QueryParamsDTO()
        {
            Attribute = "name",
            Order = "desc",
            PageNumber = 1,
            PageSize = 2,
        };
        
        _dogManagerMock
            .Setup(manager => manager.GetRangeAsync(It.IsAny<QueryParams>()))
            .ReturnsAsync(mockDogs.OrderByDescending(dog => dog.Name).Take(3).ToList());

        // Act
        var result = await _controller.GetRange(queryParamsDto);
        var actual = (result.Result as OkObjectResult).Value as List<DogDTO>;

        // Assert
        Assert.Equal(3, actual.Count);
        Assert.Equal(actual[0].Name, "Pepsi"); 
        Assert.Equal(actual[1].Name, "Nessy"); 
        Assert.Equal(actual[2].Name, "Neo"); 
    }
    
    [Fact]
    public async Task CreateDog_ReturnsCreatedDog()
    {
        // Arrange
        var createDogDto = new CreateDogDTO
        {
            Name = "Buddy",
            Color = "Brown",
            TailLength = 20,
            Weight = 20
        };
        
        _dogManagerMock
            .Setup(manager => manager.CreateAsync(It.IsAny<Dog>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Create(createDogDto);
        CreateDogDTO actual = (result.Result as ObjectResult).Value as CreateDogDTO;
        
        // Assert
        var returnedDogDto = Assert.IsType<CreateDogDTO>(actual);
        Assert.Equal(createDogDto.Name, returnedDogDto.Name);
        Assert.Equal(createDogDto.Color, returnedDogDto.Color);
        Assert.Equal(createDogDto.TailLength, returnedDogDto.TailLength);
        Assert.Equal(createDogDto.Weight, returnedDogDto.Weight);
    }

}