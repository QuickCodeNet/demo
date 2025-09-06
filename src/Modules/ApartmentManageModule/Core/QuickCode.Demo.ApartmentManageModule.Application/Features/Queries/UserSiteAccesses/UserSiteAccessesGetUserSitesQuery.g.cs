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
    public class UserSiteAccessesGetUserSitesQuery : IRequest<Response<List<UserSiteAccessesGetUserSitesResponseDto>>>
    {
        public int UserSiteAccessesUserId { get; set; }
        public bool UserSiteAccessesIsActive { get; set; }

        public UserSiteAccessesGetUserSitesQuery(int userSiteAccessesUserId, bool userSiteAccessesIsActive)
        {
            this.UserSiteAccessesUserId = userSiteAccessesUserId;
            this.UserSiteAccessesIsActive = userSiteAccessesIsActive;
        }

        public class UserSiteAccessesGetUserSitesHandler : IRequestHandler<UserSiteAccessesGetUserSitesQuery, Response<List<UserSiteAccessesGetUserSitesResponseDto>>>
        {
            private readonly ILogger<UserSiteAccessesGetUserSitesHandler> _logger;
            private readonly IUserSiteAccessesRepository _repository;
            public UserSiteAccessesGetUserSitesHandler(ILogger<UserSiteAccessesGetUserSitesHandler> logger, IUserSiteAccessesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<UserSiteAccessesGetUserSitesResponseDto>>> Handle(UserSiteAccessesGetUserSitesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UserSiteAccessesGetUserSitesAsync(request.UserSiteAccessesUserId, request.UserSiteAccessesIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}