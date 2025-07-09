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
    public class RefreshTokensInsertCommand : IRequest<Response<RefreshTokensDto>>
    {
        public RefreshTokensDto request { get; set; }

        public RefreshTokensInsertCommand(RefreshTokensDto request)
        {
            this.request = request;
        }

        public class RefreshTokensInsertHandler : IRequestHandler<RefreshTokensInsertCommand, Response<RefreshTokensDto>>
        {
            private readonly ILogger<RefreshTokensInsertHandler> _logger;
            private readonly IRefreshTokensRepository _repository;
            public RefreshTokensInsertHandler(ILogger<RefreshTokensInsertHandler> logger, IRefreshTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<RefreshTokensDto>> Handle(RefreshTokensInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}