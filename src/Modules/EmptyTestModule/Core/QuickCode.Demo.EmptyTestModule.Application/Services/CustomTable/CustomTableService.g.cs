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
    public partial class CustomTableService : ICustomTableService
    {
        private readonly ILogger<CustomTableService> _logger;
        private readonly ICustomTableRepository _repository;
        public CustomTableService(ILogger<CustomTableService> logger, ICustomTableRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CustomTableDto>> InsertAsync(CustomTableDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CustomTableDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CustomTableDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CustomTableDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CustomTableDto>> GetItemAsync(int id)
        {
            var returnValue = await _repository.GetByPkAsync(id);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteItemAsync(int id)
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
    }
}