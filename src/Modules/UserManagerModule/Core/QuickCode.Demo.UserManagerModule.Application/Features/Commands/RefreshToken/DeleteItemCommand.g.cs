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
    public class DeleteItemRefreshTokenCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public DeleteItemRefreshTokenCommand(int id)
        {
            this.Id = id;
        }

        public class DeleteItemRefreshTokenHandler : IRequestHandler<DeleteItemRefreshTokenCommand, Response<bool>>
        {
            private readonly ILogger<DeleteItemRefreshTokenHandler> _logger;
            private readonly IRefreshTokenRepository _repository;
            public DeleteItemRefreshTokenHandler(ILogger<DeleteItemRefreshTokenHandler> logger, IRefreshTokenRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(DeleteItemRefreshTokenCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
                if (deleteItem.Code == 404)
                    return Response<bool>.NotFound();
                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}