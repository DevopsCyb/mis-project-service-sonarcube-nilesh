using Microsoft.AspNetCore.Mvc;
using Moq;
using MIS.Services.Projects.Api.Controllers;
using MIS.Services.Projects.Api.Models;
using MIS.Services.Projects.Api.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIS.Services.Project.Tests.Controller
{
    public class RoleControllerTests
    {
        private readonly RolesController _controller;
        private readonly Mock<IRolesRepository> _rolesRepositoryMock;
        public RoleControllerTests() {
            _rolesRepositoryMock = new Mock<IRolesRepository>();
            _controller = new RolesController(_rolesRepositoryMock.Object);
        }

        [Fact]
        public async Task GetRole_ReturnsOkResult()
        {
            // Arrange
            var expected = new List<Role>
            {
                new Role
                {
                    RoleId = 1, RoleName = "Manager"
                },
                new Role {
                    RoleId = 2, RoleName = "Tester"
                }
            };

            _rolesRepositoryMock.Setup(x => x.GetRoles()).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetRoles();

            // Assert

            Assert.IsType<ActionResult<IEnumerable<Role>>>(result);
        }
    }
}
