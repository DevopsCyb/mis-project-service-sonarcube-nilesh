using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json.Linq;
using MIS.Services.Projects.Api.Controllers;
using MIS.Services.Projects.Api.DTOs;
using MIS.Services.Projects.Api.Models;
using MIS.Services.Projects.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MIS.Services.Projects.Tests.Controller
{
    public class ProjectControllerTests
    {
        private readonly Mock<IProjectRepository> _mockRepo;
        private readonly ProjectsController _controller;
        //private readonly Project project;
        public ProjectControllerTests()
        {
            _mockRepo = new Mock<IProjectRepository>();
            var context = new Mock<ProjectdbContext>();
            _controller = new ProjectsController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetProjects_ShouldReturnOkResultWithProject()
        {
            // Arrange
            var mockProjectRepository = new Mock<IProjectRepository>();
            mockProjectRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(GetProjectsData());

            var controller = new ProjectsController(mockProjectRepository.Object);

            // Act
            var result = await controller.GetProjects();

            
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
            //Assert.Equal(GetProjectsData(), result);

        }

        private IEnumerable<ProjectRequestDto> GetProjectsData()
        {
            return new List<ProjectRequestDto>
            {
                new ProjectRequestDto
        {
            ProjectId = 1,
            CustomerId = 100,
            ProjectName = "Project1",
            ManagerId = 200,
            VerticalName = "Sales",
            ProjectResources = new List<ProjectResourcesRequestDto>
            {
                new ProjectResourcesRequestDto
                {
                    ResourceId = 1,
                    EmployeeId = 1000,
                    EndDate = new DateTime(2022, 12, 31),
                    StartDate = new DateTime(2022, 01, 01),
                    RoleName = "Manager"
                }
            }
        },
        new ProjectRequestDto
        {
            ProjectId = 2,
            CustomerId = 200,
            ProjectName = "Project2",
            ManagerId = 300,
            VerticalName = "Marketing",
            ProjectResources = new List<ProjectResourcesRequestDto>
            {
                new ProjectResourcesRequestDto
                {
                    ResourceId = 2,
                    EmployeeId = 2000,
                    EndDate = new DateTime(2023, 01, 31),
                    StartDate = new DateTime(2022, 05, 01),
                    RoleName = "Developer"
                }
            }
        },
        new ProjectRequestDto
        {
            ProjectId = 3,
            CustomerId = 300,
            ProjectName = "Project3",
            ManagerId = 400,
            VerticalName = "Technology",
            ProjectResources = new List<ProjectResourcesRequestDto>
            {
                new ProjectResourcesRequestDto
                {
                    ResourceId = 3,
                    EmployeeId = 3000,
                    EndDate = new DateTime(2022, 09, 30),
                    StartDate = new DateTime(2022, 02, 01),
                    RoleName = "Tester"
                }
            }
        }
            };
        }

        //[Fact]
        //public async Task Post_Project_Returns_CreatedAtActionResult()
        //{
        //    // Arrange
        //    var newProject = new ProjectResponseDto
        //    {
        //        CustomerId = 1,
        //        ProjectName = "Test Project",
        //        ManagerId = 1,
        //        StartDate = DateTime.Now,
        //        EndDate = DateTime.Now.AddDays(30),
        //        verticalId = 1
        //    };

        //    _mockRepo.Setup(repo => repo.AddProjectAsync(It.IsAny<ProjectResponseDto>()))
        //        .ReturnsAsync(new Project { ProjectId = 1 });

        //    // Act
        //    var result = await _controller.PostProject(newProject);

        //    // Assert
        //    var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        //    var returnValue = Assert.IsType<ProjectResponseDto>(createdAtActionResult.Value);
        //    Assert.Equal("Test Project", returnValue.ProjectName);
        //}



    }
}
