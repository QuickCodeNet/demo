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
    public class UserSiteAccessesGetUserFlatsQuery : IRequest<Response<List<UserSiteAccessesGetUserFlatsResponseDto>>>
    {
        public int UserSiteAccessesUserId { get; set; }
        public bool UserSiteAccessesIsActive { get; set; }

        public UserSiteAccessesGetUserFlatsQuery(int userSiteAccessesUserId, bool userSiteAccessesIsActive)
        {
            this.UserSiteAccessesUserId = userSiteAccessesUserId;
            this.UserSiteAccessesIsActive = userSiteAccessesIsActive;
        }

        public class UserSiteAccessesGetUserFlatsHandler : IRequestHandler<UserSiteAccessesGetUserFlatsQuery, Response<List<UserSiteAccessesGetUserFlatsResponseDto>>>
        {
            private readonly ILogger<UserSiteAccessesGetUserFlatsHandler> _logger;
            private readonly IUserSiteAccessesRepository _repository;
            public UserSiteAccessesGetUserFlatsHandler(ILogger<UserSiteAccessesGetUserFlatsHandler> logger, IUserSiteAccessesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<UserSiteAccessesGetUserFlatsResponseDto>>> Handle(UserSiteAccessesGetUserFlatsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.UserSiteAccessesGetUserFlatsAsync(request.UserSiteAccessesUserId, request.UserSiteAccessesIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}