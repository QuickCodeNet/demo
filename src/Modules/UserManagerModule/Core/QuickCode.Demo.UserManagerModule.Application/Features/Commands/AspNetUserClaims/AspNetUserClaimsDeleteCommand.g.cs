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
    public class AspNetUserClaimsDeleteCommand : IRequest<Response<bool>>
    {
        public AspNetUserClaimsDto request { get; set; }

        public AspNetUserClaimsDeleteCommand(AspNetUserClaimsDto request)
        {
            this.request = request;
        }

        public class AspNetUserClaimsDeleteHandler : IRequestHandler<AspNetUserClaimsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserClaimsDeleteHandler> _logger;
            private readonly IAspNetUserClaimsRepository _repository;
            public AspNetUserClaimsDeleteHandler(ILogger<AspNetUserClaimsDeleteHandler> logger, IAspNetUserClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserClaimsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}