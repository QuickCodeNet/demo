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
    public partial interface ICompanyService
    {
        Task<Response<CompanyDto>> InsertAsync(CompanyDto request);
        Task<Response<bool>> DeleteAsync(CompanyDto request);
        Task<Response<bool>> UpdateAsync(int id, CompanyDto request);
        Task<Response<List<CompanyDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CompanyDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetCompaniesResponseDto>>> GetCompaniesAsync(int companiesId);
        Task<Response<List<GetUsersForCompaniesResponseDto>>> GetUsersForCompaniesAsync(int companiesId);
        Task<Response<GetUsersForCompaniesResponseDto>> GetUsersForCompaniesDetailsAsync(int companiesId, int usersId);
    }
}