using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ProductCatalogModule.Domain.Entities;
using QuickCode.Demo.ProductCatalogModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ProductCatalogModule.Application.Dtos.AuditLog;
using QuickCode.Demo.ProductCatalogModule.Domain.Enums;

namespace QuickCode.Demo.ProductCatalogModule.Application.Services.AuditLog
{
    public partial class AuditLogService : IAuditLogService
    {
        private readonly ILogger<AuditLogService> _logger;
        private readonly IAuditLogRepository _repository;
        public AuditLogService(ILogger<AuditLogService> logger, IAuditLogRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<AuditLogDto>> InsertAsync(AuditLogDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(AuditLogDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(Guid id, AuditLogDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<AuditLogDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<AuditLogDto>> GetItemAsync(Guid id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(Guid id)
        {
            var deleteItem = await _repository.GetByPkAsync(id);
            if (deleteItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.DeleteAsync(deleteItem.Value);
            return returnValue.ToResponse();
        }

        public async Task<Response<int>> TotalItemCountAsync()
        {
            var returnValue = await _repository.CountAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetRecentHttpRequestsResponseDto>>> GetRecentHttpRequestsAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetRecentHttpRequestsAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetRecentHttpRequestsByModuleResponseDto>>> GetRecentHttpRequestsByModuleAsync(string? auditLogRequestModule, int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.GetRecentHttpRequestsByModuleAsync(auditLogRequestModule, pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<StatsTodayHttpRequestsResponseDto>> StatsTodayHttpRequestsAsync()
        {
            var returnValue = await _repository.StatsTodayHttpRequestsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<StatsLastHourHttpRequestsResponseDto>> StatsLastHourHttpRequestsAsync()
        {
            var returnValue = await _repository.StatsLastHourHttpRequestsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<StatsYesterdayHttpRequestsResponseDto>> StatsYesterdayHttpRequestsAsync()
        {
            var returnValue = await _repository.StatsYesterdayHttpRequestsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<StatsWeeklyHttpRequestsResponseDto>> StatsWeeklyHttpRequestsAsync()
        {
            var returnValue = await _repository.StatsWeeklyHttpRequestsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<StatsMonthlyHttpRequestsResponseDto>> StatsMonthlyHttpRequestsAsync()
        {
            var returnValue = await _repository.StatsMonthlyHttpRequestsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<StatsYearlyHttpRequestsResponseDto>> StatsYearlyHttpRequestsAsync()
        {
            var returnValue = await _repository.StatsYearlyHttpRequestsAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StatsDailyHttpRequestsByDayResponseDto>>> StatsDailyHttpRequestsByDayAsync()
        {
            var returnValue = await _repository.StatsDailyHttpRequestsByDayAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StatsDailyHttpRequestsByDayByModuleResponseDto>>> StatsDailyHttpRequestsByDayByModuleAsync()
        {
            var returnValue = await _repository.StatsDailyHttpRequestsByDayByModuleAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StatsHourlyHttpRequestsByModuleResponseDto>>> StatsHourlyHttpRequestsByModuleAsync()
        {
            var returnValue = await _repository.StatsHourlyHttpRequestsByModuleAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StatsTotalHttpRequestsByModuleResponseDto>>> StatsTotalHttpRequestsByModuleAsync()
        {
            var returnValue = await _repository.StatsTotalHttpRequestsByModuleAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StatsHttpStatusCodesLast24HoursByModuleResponseDto>>> StatsHttpStatusCodesLast24HoursByModuleAsync()
        {
            var returnValue = await _repository.StatsHttpStatusCodesLast24HoursByModuleAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StatsHttpStatusCodesResponseDto>>> StatsHttpStatusCodesAsync()
        {
            var returnValue = await _repository.StatsHttpStatusCodesAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StatsHttpStatusCodesByModuleResponseDto>>> StatsHttpStatusCodesByModuleAsync()
        {
            var returnValue = await _repository.StatsHttpStatusCodesByModuleAsync();
            return returnValue.ToResponse();
        }

        public async Task<Response<List<StatsTopHttpServicesByModuleResponseDto>>> StatsTopHttpServicesByModuleAsync()
        {
            var returnValue = await _repository.StatsTopHttpServicesByModuleAsync();
            return returnValue.ToResponse();
        }
    }
}