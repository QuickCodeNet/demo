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
    public partial interface ICompanyInfoService
    {
        Task<Response<CompanyInfoDto>> InsertAsync(CompanyInfoDto request);
        Task<Response<bool>> DeleteAsync(CompanyInfoDto request);
        Task<Response<bool>> UpdateAsync(int id, CompanyInfoDto request);
        Task<Response<List<CompanyInfoDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<CompanyInfoDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetCompanyContactResponseDto>>> GetCompanyContactAsync(int companyInfosId);
    }
}