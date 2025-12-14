using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.OnlineShopModule.Domain.Entities;
using QuickCode.Demo.OnlineShopModule.Application.Interfaces.Repositories;
using QuickCode.Demo.OnlineShopModule.Application.Dtos.HomePage;
using QuickCode.Demo.OnlineShopModule.Domain.Enums;

namespace QuickCode.Demo.OnlineShopModule.Application.Services.HomePage
{
    public partial interface IHomePageService
    {
        Task<Response<HomePageDto>> InsertAsync(HomePageDto request);
        Task<Response<bool>> DeleteAsync(HomePageDto request);
        Task<Response<bool>> UpdateAsync(int id, HomePageDto request);
        Task<Response<List<HomePageDto>>> ListAsync(int? pageNumber, int? pageSize);
        Task<Response<HomePageDto>> GetItemAsync(int id);
        Task<Response<bool>> DeleteItemAsync(int id);
        Task<Response<int>> TotalItemCountAsync();
        Task<Response<List<GetHomePageContentResponseDto>>> GetHomePageContentAsync(string homePagesTitleImageUrl);
        Task<Response<List<GetHomePageContentGrResponseDto>>> GetHomePageContentGrAsync(string homePagesTitleImageUrl);
    }
}