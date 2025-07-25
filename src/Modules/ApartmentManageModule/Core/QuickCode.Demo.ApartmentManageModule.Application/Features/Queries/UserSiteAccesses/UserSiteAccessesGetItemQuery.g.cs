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
    public class UserSiteAccessesGetItemQuery : IRequest<Response<UserSiteAccessesDto>>
    {
        public int Id { get; set; }

        public UserSiteAccessesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class UserSiteAccessesGetItemHandler : IRequestHandler<UserSiteAccessesGetItemQuery, Response<UserSiteAccessesDto>>
        {
            private readonly ILogger<UserSiteAccessesGetItemHandler> _logger;
            private readonly IUserSiteAccessesRepository _repository;
            public UserSiteAccessesGetItemHandler(ILogger<UserSiteAccessesGetItemHandler> logger, IUserSiteAccessesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<UserSiteAccessesDto>> Handle(UserSiteAccessesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}