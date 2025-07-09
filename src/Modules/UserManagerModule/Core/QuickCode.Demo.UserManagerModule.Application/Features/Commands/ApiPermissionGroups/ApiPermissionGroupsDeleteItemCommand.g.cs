using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class ApiPermissionGroupsDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public ApiPermissionGroupsDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class ApiPermissionGroupsDeleteItemHandler : IRequestHandler<ApiPermissionGroupsDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<ApiPermissionGroupsDeleteItemHandler> _logger;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsDeleteItemHandler(ILogger<ApiPermissionGroupsDeleteItemHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ApiPermissionGroupsDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}