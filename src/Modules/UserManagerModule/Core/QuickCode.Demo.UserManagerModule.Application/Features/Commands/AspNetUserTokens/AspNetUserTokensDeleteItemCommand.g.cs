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
    public class AspNetUserTokensDeleteItemCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }

        public AspNetUserTokensDeleteItemCommand(string userId)
        {
            this.UserId = userId;
        }

        public class AspNetUserTokensDeleteItemHandler : IRequestHandler<AspNetUserTokensDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserTokensDeleteItemHandler> _logger;
            private readonly IAspNetUserTokensRepository _repository;
            public AspNetUserTokensDeleteItemHandler(ILogger<AspNetUserTokensDeleteItemHandler> logger, IAspNetUserTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserTokensDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.UserId);
                if (deleteItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var returnValue = await _repository.DeleteAsync(deleteItem.Value);
                return returnValue.ToResponse();
            }
        }
    }
}