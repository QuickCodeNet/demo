using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Features
{
    public class UserSiteAccessesListQuery : IRequest<Response<List<UserSiteAccessesDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public UserSiteAccessesListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class UserSiteAccessesListHandler : IRequestHandler<UserSiteAccessesListQuery, Response<List<UserSiteAccessesDto>>>
        {
            private readonly ILogger<UserSiteAccessesListHandler> _logger;
            private readonly IUserSiteAccessesRepository _repository;
            public UserSiteAccessesListHandler(ILogger<UserSiteAccessesListHandler> logger, IUserSiteAccessesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<UserSiteAccessesDto>>> Handle(UserSiteAccessesListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}