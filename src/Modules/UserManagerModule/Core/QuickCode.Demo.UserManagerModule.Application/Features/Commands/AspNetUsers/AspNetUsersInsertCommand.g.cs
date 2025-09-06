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
    public class AspNetUsersInsertCommand : IRequest<Response<AspNetUsersDto>>
    {
        public AspNetUsersDto request { get; set; }

        public AspNetUsersInsertCommand(AspNetUsersDto request)
        {
            this.request = request;
        }

        public class AspNetUsersInsertHandler : IRequestHandler<AspNetUsersInsertCommand, Response<AspNetUsersDto>>
        {
            private readonly ILogger<AspNetUsersInsertHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersInsertHandler(ILogger<AspNetUsersInsertHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUsersDto>> Handle(AspNetUsersInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}