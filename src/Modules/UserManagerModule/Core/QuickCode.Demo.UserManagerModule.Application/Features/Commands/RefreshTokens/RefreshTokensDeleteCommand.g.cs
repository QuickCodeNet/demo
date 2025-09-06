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
    public class RefreshTokensDeleteCommand : IRequest<Response<bool>>
    {
        public RefreshTokensDto request { get; set; }

        public RefreshTokensDeleteCommand(RefreshTokensDto request)
        {
            this.request = request;
        }

        public class RefreshTokensDeleteHandler : IRequestHandler<RefreshTokensDeleteCommand, Response<bool>>
        {
            private readonly ILogger<RefreshTokensDeleteHandler> _logger;
            private readonly IRefreshTokensRepository _repository;
            public RefreshTokensDeleteHandler(ILogger<RefreshTokensDeleteHandler> logger, IRefreshTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(RefreshTokensDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}