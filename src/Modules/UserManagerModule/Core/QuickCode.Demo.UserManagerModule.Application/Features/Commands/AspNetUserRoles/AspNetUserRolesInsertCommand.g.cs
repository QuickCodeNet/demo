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
    public class AspNetUserRolesInsertCommand : IRequest<Response<AspNetUserRolesDto>>
    {
        public AspNetUserRolesDto request { get; set; }

        public AspNetUserRolesInsertCommand(AspNetUserRolesDto request)
        {
            this.request = request;
        }

        public class AspNetUserRolesInsertHandler : IRequestHandler<AspNetUserRolesInsertCommand, Response<AspNetUserRolesDto>>
        {
            private readonly ILogger<AspNetUserRolesInsertHandler> _logger;
            private readonly IAspNetUserRolesRepository _repository;
            public AspNetUserRolesInsertHandler(ILogger<AspNetUserRolesInsertHandler> logger, IAspNetUserRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUserRolesDto>> Handle(AspNetUserRolesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}