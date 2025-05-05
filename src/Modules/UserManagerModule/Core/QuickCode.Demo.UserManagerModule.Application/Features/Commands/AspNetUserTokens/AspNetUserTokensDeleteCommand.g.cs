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
    public class AspNetUserTokensDeleteCommand : IRequest<Response<bool>>
    {
        public AspNetUserTokensDto request { get; set; }

        public AspNetUserTokensDeleteCommand(AspNetUserTokensDto request)
        {
            this.request = request;
        }

        public class AspNetUserTokensDeleteHandler : IRequestHandler<AspNetUserTokensDeleteCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserTokensDeleteHandler> _logger;
            private readonly IAspNetUserTokensRepository _repository;
            public AspNetUserTokensDeleteHandler(ILogger<AspNetUserTokensDeleteHandler> logger, IAspNetUserTokensRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserTokensDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}