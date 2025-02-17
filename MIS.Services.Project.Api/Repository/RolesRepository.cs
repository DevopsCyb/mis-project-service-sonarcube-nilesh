using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ProjectdbContext _context;

        public RolesRepository(ProjectdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            if (_context.Roles == null)
            {
                return null;
            }
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetRole(int id)
        {
            if (_context.Roles == null)
            {
                return null;
            }
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                return null;
            }

            return role;
        }

        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            if (_context.Roles == null)
            {
                return null;
            }
            _context.Roles.Add(role);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RoleExists(role.RoleId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return role;
        }

        public async Task<IActionResult> PutRole(int id, Role role)
        {
            if (id != role.RoleId)
            {
                return null;
            }

            _context.Entry(role).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return null;
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            if (_context.Roles == null)
            {
                return null;
            }
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return null;
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return null;
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public bool RoleExists(int id)
        {
            return (_context.Roles?.Any(e => e.RoleId == id)).GetValueOrDefault();
        }


    }
}
