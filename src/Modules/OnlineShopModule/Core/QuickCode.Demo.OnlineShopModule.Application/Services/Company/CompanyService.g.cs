using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.Company;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.Company
{
    public partial class CompanyService : ICompanyService
    {
        private readonly ILogger<CompanyService> _logger;
        private readonly ICompanyRepository _repository;
        public CompanyService(ILogger<CompanyService> logger, ICompanyRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<CompanyDto>> InsertAsync(CompanyDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(CompanyDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, CompanyDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<CompanyDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<CompanyDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetCompaniesResponseDto>>> GetCompaniesAsync(int companiesId)
        {
            var returnValue = await _repository.GetCompaniesAsync(companiesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetUsersForCompaniesResponseDto>>> GetUsersForCompaniesAsync(int companiesId)
        {
            var returnValue = await _repository.GetUsersForCompaniesAsync(companiesId);
            return returnValue.ToResponse();
        }

        public async Task<Response<GetUsersForCompaniesResponseDto>> GetUsersForCompaniesDetailsAsync(int companiesId, int usersId)
        {
            var returnValue = await _repository.GetUsersForCompaniesDetailsAsync(companiesId, usersId);
            return returnValue.ToResponse();
        }
    }
}