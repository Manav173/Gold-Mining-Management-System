using Gold_Mining_Management_System.Models;

namespace Gold_Mining_Management_System.Repositories
{
    public interface IReportRepository
    {
        public Task<IEnumerable<Reports>> GetAllReportsAsync();
        public Task<Reports> GetReportByIdAsync(int? id);
        public Task AddReportAsync(Reports report);
        public Task UpdateReportAsync(Reports report);
        public Task DeleteReportAsync(int id);
    }
}
