using Microsoft.EntityFrameworkCore;
using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.Repository
{
    public class VerticalRepository : IVerticalRepository
    {
        private readonly ProjectdbContext _context;

        public VerticalRepository(ProjectdbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vertical>> GetVerticals()
        {
            return await _context.Verticals.ToListAsync();
        }

        public async Task<Vertical> GetVertical(int id)
        {
            var vertical = await _context.Verticals.FindAsync(id);
            return vertical;
        }

        public async Task<Vertical> PutVertical(int id, Vertical vertical)
        {
            _context.Entry(vertical).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return vertical;
        }

        public async Task<Vertical> PostVertical(Vertical vertical)
        {
            _context.Verticals.Add(vertical);
            await _context.SaveChangesAsync();
            return vertical;
        }

        public async Task DeleteVertical(int id)
        {
            var vertical = await _context.Verticals.FindAsync(id);
            _context.Verticals.Remove(vertical);
            await _context.SaveChangesAsync();
        }

        public bool VerticalExists(int id)
        {
            return _context.Verticals.Any(e => e.VerticalId == id);
        }
    }
}
