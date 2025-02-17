using Microsoft.AspNetCore.Mvc;
using Moq;
using MIS.Services.Projects.Api.Controllers;
using MIS.Services.Projects.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MIS.Services.Projects.Api.Repository;

namespace MIS.Services.Projects.Api.Controller
{
    public class VerticalControllerTests
    {
        private readonly VerticalsController _controller;
        private readonly Mock<IVerticalRepository> _verticalsRepositoryMock;

        public VerticalControllerTests()
        {
            _verticalsRepositoryMock = new Mock<IVerticalRepository>();
            _controller = new VerticalsController(_verticalsRepositoryMock.Object);
        }

        [Fact]
        public async Task GetVertical_ReturnsOkResult()
        {
            // Arrange
            var expected = new List<Vertical>
            {
                new Vertical
                {
                    VerticalId = 1, VerticalName = "IT"
                },
                new Vertical {
                    VerticalId = 2, VerticalName = "IT"
                }
            };
                 
            _verticalsRepositoryMock.Setup(x => x.GetVerticals()).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetVerticals();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<Vertical>>>(result);
        }

        

        //[Fact]
        //public async Task PostVertical_ReturnsCreatedResult()
        //{
        //    // Arrange
        //    var expected = new Vertical { VerticalId = 1, VerticalName = "IT" };
        //    _verticalsRepositoryMock.Setup(x => x.PostVertical(expected)).Returns(Task.FromResult(true));

        //    // Act
        //    var result = await _controller.PostVertical(expected);

        //    // Assert
        //    var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        //    var returnValue = Assert.IsType<Vertical>(createdResult.Value);
        //    Assert.Equal(expected, returnValue);
        //}

        //[Fact]
        //public async Task PostVertical_ReturnsConflictResult()
        //{
        //    // Arrange
        //    var expected = new Vertical { VerticalId = 1, VerticalName = "IT" };
        //    _verticalsRepositoryMock.Setup(x => x.PostVertical(expected)).Returns(Task.FromResult(true));

        //    // Act
        //    var result = await _controller.PostVertical(expected);

        //    // Assert
        //    Assert.IsType<ConflictResult>(result);
        //}
    }
}
