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
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features
{
    public class AspNetRoleClaimsDeleteCommand : IRequest<Response<bool>>
    {
        public AspNetRoleClaimsDto request { get; set; }

        public AspNetRoleClaimsDeleteCommand(AspNetRoleClaimsDto request)
        {
            this.request = request;
        }

        public class AspNetRoleClaimsDeleteHandler : IRequestHandler<AspNetRoleClaimsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<AspNetRoleClaimsDeleteHandler> _logger;
            private readonly IAspNetRoleClaimsRepository _repository;
            public AspNetRoleClaimsDeleteHandler(ILogger<AspNetRoleClaimsDeleteHandler> logger, IAspNetRoleClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetRoleClaimsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}