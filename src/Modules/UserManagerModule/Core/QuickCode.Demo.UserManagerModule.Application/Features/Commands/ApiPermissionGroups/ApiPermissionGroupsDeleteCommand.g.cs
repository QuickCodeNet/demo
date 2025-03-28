using AutoMapper;
using System.Linq;
using MediatR;
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
    public class ApiPermissionGroupsDeleteCommand : IRequest<Response<bool>>
    {
        public ApiPermissionGroupsDto request { get; set; }

        public ApiPermissionGroupsDeleteCommand(ApiPermissionGroupsDto request)
        {
            this.request = request;
        }

        public class ApiPermissionGroupsDeleteHandler : IRequestHandler<ApiPermissionGroupsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<ApiPermissionGroupsDeleteHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IApiPermissionGroupsRepository _repository;
            public ApiPermissionGroupsDeleteHandler(IMapper mapper, ILogger<ApiPermissionGroupsDeleteHandler> logger, IApiPermissionGroupsRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ApiPermissionGroupsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = _mapper.Map<ApiPermissionGroups>(request.request);
                var returnValue = _mapper.Map<Response<bool>>(await _repository.DeleteAsync(model));
                return returnValue;
            }
        }
    }
}