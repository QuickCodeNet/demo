using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.CompanyInfo;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.CompanyInfo
{
    public partial class CompanyInfoService : ICompanyInfoService
    {
        private readonly ILogger<CompanyInfoService> _logger;
        private readonly ICompanyInfoRepository _repository;
        public CompanyInfoService(ILogger<CompanyInfoService> logger, ICompanyInfoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CompanyInfoDto>> InsertAsync(CompanyInfoDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CompanyInfoDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CompanyInfoDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CompanyInfoDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CompanyInfoDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetCompanyContactResponseDto>>> GetCompanyContactAsync(int companyInfosId)
        {
            var returnValue = await _repository.GetCompanyContactAsync(companyInfosId);
            return returnValue.ToResponse();
        }
    }
}