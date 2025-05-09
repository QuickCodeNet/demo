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
    public class AspNetRolesInsertCommand : IRequest<Response<AspNetRolesDto>>
    {
        public AspNetRolesDto request { get; set; }

        public AspNetRolesInsertCommand(AspNetRolesDto request)
        {
            this.request = request;
        }

        public class AspNetRolesInsertHandler : IRequestHandler<AspNetRolesInsertCommand, Response<AspNetRolesDto>>
        {
            private readonly ILogger<AspNetRolesInsertHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesInsertHandler(ILogger<AspNetRolesInsertHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetRolesDto>> Handle(AspNetRolesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}