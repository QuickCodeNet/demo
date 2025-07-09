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
    public class AspNetUserRolesDeleteCommand : IRequest<Response<bool>>
    {
        public AspNetUserRolesDto request { get; set; }

        public AspNetUserRolesDeleteCommand(AspNetUserRolesDto request)
        {
            this.request = request;
        }

        public class AspNetUserRolesDeleteHandler : IRequestHandler<AspNetUserRolesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserRolesDeleteHandler> _logger;
            private readonly IAspNetUserRolesRepository _repository;
            public AspNetUserRolesDeleteHandler(ILogger<AspNetUserRolesDeleteHandler> logger, IAspNetUserRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserRolesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}