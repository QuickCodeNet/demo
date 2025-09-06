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
    public class RefreshTokensUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public RefreshTokensDto request { get; set; }

        public RefreshTokensUpdateCommand(int id, RefreshTokensDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class RefreshTokensUpdateHandler : IRequestHandler<RefreshTokensUpdateCommand, Response<bool>>
        {
            private readonly ILogger<RefreshTokensUpdateHandler> _logger;
            private readonly IRefreshTokensRepository _repository;
            public RefreshTokensUpdateHandler(ILogger<RefreshTokensUpdateHandler> logger, IRefreshTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(RefreshTokensUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}