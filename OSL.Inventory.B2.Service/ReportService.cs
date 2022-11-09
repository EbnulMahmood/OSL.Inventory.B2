using OSL.Inventory.B2.Repository;
using OSL.Inventory.B2.Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSL.Inventory.B2.Service
{
    public interface IReportService
    {
        Task<IEnumerable<ReportDto>> ProfitAndLossSummaryServiceAsync();
    }

    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportDto>> ProfitAndLossSummaryServiceAsync()
        {
            return null;
        }

        public Task<IEnumerable<SalesPerDayDto>> ListSalesPerDayServiceAsync()
        {
            var result = new List<SalesPerDayDto>();
            return null;
        }
    }
}
