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
        List<Dog> mockDogs =
        [
            new() { Id = 1, Name = "Neo", Color = "Red & Amber", TailLength = 22, Weight = 32 },
            new() { Id = 2, Name = "Jessy", Color = "Black & White", TailLength = 7, Weight = 14 },
        ];
        _dogManagerMock.Setup(service => service.GetRangeAsync(null).Result).Returns(mockDogs);

        // Act
        var result = await _controller.GetRange(null);
        IEnumerable<DogDTO> actual = (result.Result as OkObjectResult).Value as IEnumerable<DogDTO>;

        // Assert
        Assert.IsType<ActionResult<IEnumerable<DogDTO>>>(result);
        Assert.Equal(2, actual.Count());
    }
}