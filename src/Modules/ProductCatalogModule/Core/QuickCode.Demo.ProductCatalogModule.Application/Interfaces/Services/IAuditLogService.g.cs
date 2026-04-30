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
    public partial interface IAuditLogService
    {
        Task<Response<AuditLogDto>> InsertAsync(AuditLogDto request);
        Task<Response<bool>> DeleteAsync(AuditLogDto request);
        Task<Response<bool>> UpdateAsync(Guid id, AuditLogDto request);
        Task<Response<List<AuditLogDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<AuditLogDto>> GetItemAsync(Guid id);
        Task<Response<bool>> DeleteItemAsync(Guid id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetRecentHttpRequestsResponseDto>>> GetRecentHttpRequestsAsync(int? pageNumber, int? pageSize);
        Task<Response<List<GetRecentHttpRequestsByModuleResponseDto>>> GetRecentHttpRequestsByModuleAsync(string? auditLogRequestModule, int? pageNumber, int? pageSize);
        Task<Response<StatsTodayHttpRequestsResponseDto>> StatsTodayHttpRequestsAsync();
        Task<Response<StatsLastHourHttpRequestsResponseDto>> StatsLastHourHttpRequestsAsync();
        Task<Response<StatsYesterdayHttpRequestsResponseDto>> StatsYesterdayHttpRequestsAsync();
        Task<Response<StatsWeeklyHttpRequestsResponseDto>> StatsWeeklyHttpRequestsAsync();
        Task<Response<StatsMonthlyHttpRequestsResponseDto>> StatsMonthlyHttpRequestsAsync();
        Task<Response<StatsYearlyHttpRequestsResponseDto>> StatsYearlyHttpRequestsAsync();
        Task<Response<List<StatsDailyHttpRequestsByDayResponseDto>>> StatsDailyHttpRequestsByDayAsync();
        Task<Response<List<StatsDailyHttpRequestsByDayByModuleResponseDto>>> StatsDailyHttpRequestsByDayByModuleAsync();
        Task<Response<List<StatsHourlyHttpRequestsByModuleResponseDto>>> StatsHourlyHttpRequestsByModuleAsync();
        Task<Response<List<StatsTotalHttpRequestsByModuleResponseDto>>> StatsTotalHttpRequestsByModuleAsync();
        Task<Response<List<StatsHttpStatusCodesLast24HoursByModuleResponseDto>>> StatsHttpStatusCodesLast24HoursByModuleAsync();
        Task<Response<List<StatsHttpStatusCodesResponseDto>>> StatsHttpStatusCodesAsync();
        Task<Response<List<StatsHttpStatusCodesByModuleResponseDto>>> StatsHttpStatusCodesByModuleAsync();
        Task<Response<List<StatsTopHttpServicesByModuleResponseDto>>> StatsTopHttpServicesByModuleAsync();
    }
}