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
    public class AspNetUserClaimsInsertCommand : IRequest<Response<AspNetUserClaimsDto>>
    {
        public AspNetUserClaimsDto request { get; set; }

        public AspNetUserClaimsInsertCommand(AspNetUserClaimsDto request)
        {
            this.request = request;
        }

        public class AspNetUserClaimsInsertHandler : IRequestHandler<AspNetUserClaimsInsertCommand, Response<AspNetUserClaimsDto>>
        {
            private readonly ILogger<AspNetUserClaimsInsertHandler> _logger;
            private readonly IAspNetUserClaimsRepository _repository;
            public AspNetUserClaimsInsertHandler(ILogger<AspNetUserClaimsInsertHandler> logger, IAspNetUserClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUserClaimsDto>> Handle(AspNetUserClaimsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}