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
    public class AspNetUserTokensInsertCommand : IRequest<Response<AspNetUserTokensDto>>
    {
        public AspNetUserTokensDto request { get; set; }

        public AspNetUserTokensInsertCommand(AspNetUserTokensDto request)
        {
            this.request = request;
        }

        public class AspNetUserTokensInsertHandler : IRequestHandler<AspNetUserTokensInsertCommand, Response<AspNetUserTokensDto>>
        {
            private readonly ILogger<AspNetUserTokensInsertHandler> _logger;
            private readonly IAspNetUserTokensRepository _repository;
            public AspNetUserTokensInsertHandler(ILogger<AspNetUserTokensInsertHandler> logger, IAspNetUserTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUserTokensDto>> Handle(AspNetUserTokensInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}