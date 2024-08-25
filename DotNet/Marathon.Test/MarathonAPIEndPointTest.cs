using Marathon.DAO;
using Marathon.Controllers;
using Marathon.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Marathon.Test
{
    public class MarathonAPIEndPointTest
    {
        private readonly Mock<IIplDao> _mockIplDao;
        private readonly IPLController _controller;

        public MarathonAPIEndPointTest()
        {
            _mockIplDao = new Mock<IIplDao>();
            _controller = new IPLController(_mockIplDao.Object);

        }

        [Fact]
        public async Task InsertProduct_ReturnsBadRequest_WhenProductIsNull()
        {
            // Act
            var result = await _controller.InsertPlayer(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task InsertProduct_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            var player = new Player { PlayerId = 1, PlayerName = "Virat", TeamId=6, Role = "Batsman", Age = 30, MatchesPlayed = 222};


            // Act
            var result = await _controller.InsertPlayer(player);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Failed to add product", badRequestResult.Value);
        }

        [Fact]
        public async Task InsertProduct_Returns201()
        {
            // Arrange
            var expectedPlayer = new Player { PlayerId = 1, PlayerName = "Virat", TeamId = 6, Role = "Batsman", Age = 30, MatchesPlayed = 222 };

            _mockIplDao.Setup(dao => dao.InsertPlayer(expectedPlayer)).ReturnsAsync(1);

            // Act
            var result = await _controller.InsertPlayer(expectedPlayer);

            // Assert
            var okResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            var actualPlayer = Assert.IsAssignableFrom<Player>(okResult.Value);

        }



    }
}