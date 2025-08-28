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
    public class AspNetRolesDeleteCommand : IRequest<Response<bool>>
    {
        public AspNetRolesDto request { get; set; }

        public AspNetRolesDeleteCommand(AspNetRolesDto request)
        {
            this.request = request;
        }

        public class AspNetRolesDeleteHandler : IRequestHandler<AspNetRolesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<AspNetRolesDeleteHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesDeleteHandler(ILogger<AspNetRolesDeleteHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetRolesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}