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
    public partial class HomePageService : IHomePageService
    {
        private readonly ILogger<HomePageService> _logger;
        private readonly IHomePageRepository _repository;
        public HomePageService(ILogger<HomePageService> logger, IHomePageRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Response<HomePageDto>> InsertAsync(HomePageDto request)
        {
            var returnValue = await _repository.InsertAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> DeleteAsync(HomePageDto request)
        {
            var returnValue = await _repository.DeleteAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<bool>> UpdateAsync(int id, HomePageDto request)
        {
            var updateItem = await _repository.GetByPkAsync(request.Id);
            if (updateItem.Code == 404)
                return Response<bool>.NotFound();
            var returnValue = await _repository.UpdateAsync(request);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<HomePageDto>>> ListAsync(int? pageNumber, int? pageSize)
        {
            var returnValue = await _repository.ListAsync(pageNumber, pageSize);
            return returnValue.ToResponse();
        }

        public async Task<Response<HomePageDto>> GetItemAsync(int id)
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

        public async Task<Response<List<GetHomePageContentResponseDto>>> GetHomePageContentAsync(string homePagesTitleImageUrl)
        {
            var returnValue = await _repository.GetHomePageContentAsync(homePagesTitleImageUrl);
            return returnValue.ToResponse();
        }

        public async Task<Response<List<GetHomePageContentGrResponseDto>>> GetHomePageContentGrAsync(string homePagesTitleImageUrl)
        {
            var returnValue = await _repository.GetHomePageContentGrAsync(homePagesTitleImageUrl);
            return returnValue.ToResponse();
        }
    }
}