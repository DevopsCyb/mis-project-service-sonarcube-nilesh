using MIS.Services.Project.Api.Models;

namespace MIS.Services.Project.Api.Repository
{
    public interface IVerticalRepository
    {
        Task<IEnumerable<Vertical>> GetVerticals();
        Task<Vertical> GetVertical(int id);
        Task<Vertical> PutVertical(int id, Vertical vertical);
        Task<Vertical> PostVertical(Vertical vertical);
        Task DeleteVertical(int id);
        bool VerticalExists(int id);
    }
}
