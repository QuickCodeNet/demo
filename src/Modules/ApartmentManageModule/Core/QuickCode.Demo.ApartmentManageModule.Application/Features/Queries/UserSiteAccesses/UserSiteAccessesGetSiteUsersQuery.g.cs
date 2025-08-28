using System;
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
    public class UserSiteAccessesGetSiteUsersQuery : IRequest<Response<List<UserSiteAccessesGetSiteUsersResponseDto>>>
    {
        public int UserSiteAccessesSiteId { get; set; }
        public bool UserSiteAccessesIsActive { get; set; }

        public UserSiteAccessesGetSiteUsersQuery(int userSiteAccessesSiteId, bool userSiteAccessesIsActive)
        {
            this.UserSiteAccessesSiteId = userSiteAccessesSiteId;
            this.UserSiteAccessesIsActive = userSiteAccessesIsActive;
        }

        public class UserSiteAccessesGetSiteUsersHandler : IRequestHandler<UserSiteAccessesGetSiteUsersQuery, Response<List<UserSiteAccessesGetSiteUsersResponseDto>>>
        {
            private readonly ILogger<UserSiteAccessesGetSiteUsersHandler> _logger;
            private readonly IUserSiteAccessesRepository _repository;
            public UserSiteAccessesGetSiteUsersHandler(ILogger<UserSiteAccessesGetSiteUsersHandler> logger, IUserSiteAccessesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<UserSiteAccessesGetSiteUsersResponseDto>>> Handle(UserSiteAccessesGetSiteUsersQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UserSiteAccessesGetSiteUsersAsync(request.UserSiteAccessesSiteId, request.UserSiteAccessesIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}