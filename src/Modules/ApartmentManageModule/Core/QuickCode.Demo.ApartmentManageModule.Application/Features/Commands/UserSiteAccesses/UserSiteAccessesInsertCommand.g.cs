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
    public class UserSiteAccessesInsertCommand : IRequest<Response<UserSiteAccessesDto>>
    {
        public UserSiteAccessesDto request { get; set; }

        public UserSiteAccessesInsertCommand(UserSiteAccessesDto request)
        {
            this.request = request;
        }

        public class UserSiteAccessesInsertHandler : IRequestHandler<UserSiteAccessesInsertCommand, Response<UserSiteAccessesDto>>
        {
            private readonly ILogger<UserSiteAccessesInsertHandler> _logger;
            private readonly IUserSiteAccessesRepository _repository;
            public UserSiteAccessesInsertHandler(ILogger<UserSiteAccessesInsertHandler> logger, IUserSiteAccessesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<UserSiteAccessesDto>> Handle(UserSiteAccessesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}