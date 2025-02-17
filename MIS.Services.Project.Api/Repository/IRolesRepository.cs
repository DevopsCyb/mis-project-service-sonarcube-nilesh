using Microsoft.AspNetCore.Mvc;
using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.Repository
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRole(int id);
        Task<ActionResult<Role>> PostRole(Role role);
        Task<IActionResult> PutRole(int id, Role role);
        Task<IActionResult> DeleteRole(int id);
        bool RoleExists(int id);
        Task SaveChanges();
    }
}
