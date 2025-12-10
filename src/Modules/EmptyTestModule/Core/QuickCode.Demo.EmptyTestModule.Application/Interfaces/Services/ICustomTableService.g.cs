using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmptyTestModule.Domain.Entities;
using QuickCode.Demo.EmptyTestModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmptyTestModule.Application.Dtos.CustomTable;

namespace QuickCode.Demo.EmptyTestModule.Application.Services.CustomTable
{
    public partial interface ICustomTableService
    {
        Task<Response<CustomTableDto>> InsertAsync(CustomTableDto request);
        Task<Response<bool>> DeleteAsync(CustomTableDto request);
        Task<Response<bool>> UpdateAsync(int id, CustomTableDto request);
        Task<Response<List<CustomTableDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CustomTableDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
    }
}