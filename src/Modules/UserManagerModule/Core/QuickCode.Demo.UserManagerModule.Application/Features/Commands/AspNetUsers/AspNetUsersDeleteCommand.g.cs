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
    public class AspNetUsersDeleteCommand : IRequest<Response<bool>>
    {
        public AspNetUsersDto request { get; set; }

        public AspNetUsersDeleteCommand(AspNetUsersDto request)
        {
            this.request = request;
        }

        public class AspNetUsersDeleteHandler : IRequestHandler<AspNetUsersDeleteCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUsersDeleteHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersDeleteHandler(ILogger<AspNetUsersDeleteHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUsersDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}