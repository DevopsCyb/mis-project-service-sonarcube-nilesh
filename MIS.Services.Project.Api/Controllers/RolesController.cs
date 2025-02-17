using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS.Services.Project.Api.Models;
using MIS.Services.Project.Api.Repository;

namespace MIS.Services.Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _roleRepository;

        public RolesController(IRolesRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<IEnumerable<Role>> GetRoles()
        {
            var roles = await _roleRepository.GetRoles();
            return roles;
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var role = await _roleRepository.GetRole(id);
            if (role == null)
            {
                return NotFound();
            }

            return role;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.RoleId)
            {
                return BadRequest();
            }

            _roleRepository.PutRole(role.RoleId, role);

            try
            {
                await _roleRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_roleRepository.RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();

        }

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            if (_roleRepository.RoleExists(role.RoleId))
            {
                return BadRequest("Role already exists");
            }

            await _roleRepository.PostRole(role);

            return CreatedAtAction(nameof(GetRole), new { id = role.RoleId }, role);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            _roleRepository.DeleteRole(id);

            try
            {
                await _roleRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_roleRepository.RoleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


    }
}
