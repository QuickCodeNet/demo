using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.UserManagerModule.Domain.Entities;
using QuickCode.Demo.UserManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.UserManagerModule.Application.Dtos.RefreshToken;
using QuickCode.Demo.UserManagerModule.Domain.Enums;

namespace QuickCode.Demo.UserManagerModule.Application.Features.RefreshToken
{
    public class UpdateRefreshTokenCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public RefreshTokenDto request { get; set; }

        public UpdateRefreshTokenCommand(int id, RefreshTokenDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class UpdateRefreshTokenHandler : IRequestHandler<UpdateRefreshTokenCommand, Response<bool>>
        {
            private readonly ILogger<UpdateRefreshTokenHandler> _logger;
            private readonly IRefreshTokenRepository _repository;
            public UpdateRefreshTokenHandler(ILogger<UpdateRefreshTokenHandler> logger, IRefreshTokenRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(UpdateRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                    return Response<bool>.NotFound();
                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}