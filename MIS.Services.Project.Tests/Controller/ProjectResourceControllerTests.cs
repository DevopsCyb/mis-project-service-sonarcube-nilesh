using Microsoft.AspNetCore.Mvc;
using Moq;
using MIS.Services.Projects.Api.Controllers;
using MIS.Services.Projects.Api.DTOs;
using MIS.Services.Projects.Api.Models;
using MIS.Services.Projects.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Services.Project.Tests.Controller
{
    public class ProjectResourceControllerTests
    {
        private readonly Mock<IProjectResourcesRepository> _mockRepo;
        private readonly ProjectResourcesController _controller;

        public ProjectResourceControllerTests()
        {
            _mockRepo= new Mock<IProjectResourcesRepository>();
            _controller=new ProjectResourcesController( _mockRepo.Object );
        }

        [Fact]
        public async Task GetProjectResources_ShouldReturnOkResultWithProject()
        {
            _mockRepo.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<ProjectResourcesRequestDto>
                {
                    new ProjectResourcesRequestDto
                    {
                        EmployeeId= 1,
                        ProjectName= null,
                        ResourceId= 1,
                        RoleName= null,
                        EndDate= DateTime.Now,
                        StartDate = null
                    }
                });

            var result = await _controller.GetProjectResources();

            Assert.IsType<ActionResult<IEnumerable<ProjectResourcesRequestDto>>>(result);
        }

        //private IEnumerable<ProjectRequestDto> GetResourcesData()
        //{


        //}

    }
}
