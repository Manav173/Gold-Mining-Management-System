using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Gold_Mining_Management_System.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Reports>> GetAllReportsAsync()
        {
            return await _repository.GetAllReportsAsync();
        }

        public async Task<Reports> GetReportByIdAsync(int? id)
        {
            return await _repository.GetReportByIdAsync(id);
        }

        public async Task AddReportAsync(Reports report)
        {
            await _repository.AddReportAsync(report);
        }

        public async Task UpdateReportAsync(Reports report)
        {
            await _repository.UpdateReportAsync(report);
        }

        public async Task DeleteReportAsync(int id)
        {
            await _repository.DeleteReportAsync(id);
        }
    }
}
