using System.Linq;
using MediatR;
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
    public class AspNetRoleClaimsInsertCommand : IRequest<Response<AspNetRoleClaimsDto>>
    {
        public AspNetRoleClaimsDto request { get; set; }

        public AspNetRoleClaimsInsertCommand(AspNetRoleClaimsDto request)
        {
            this.request = request;
        }

        public class AspNetRoleClaimsInsertHandler : IRequestHandler<AspNetRoleClaimsInsertCommand, Response<AspNetRoleClaimsDto>>
        {
            private readonly ILogger<AspNetRoleClaimsInsertHandler> _logger;
            private readonly IAspNetRoleClaimsRepository _repository;
            public AspNetRoleClaimsInsertHandler(ILogger<AspNetRoleClaimsInsertHandler> logger, IAspNetRoleClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRoleClaimsDto>> Handle(AspNetRoleClaimsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}