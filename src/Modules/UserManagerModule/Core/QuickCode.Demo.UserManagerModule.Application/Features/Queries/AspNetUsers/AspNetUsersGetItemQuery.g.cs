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
    public class AspNetUsersGetItemQuery : IRequest<Response<AspNetUsersDto>>
    {
        public string Id { get; set; }

        public AspNetUsersGetItemQuery(string id)
        {
            this.Id = id;
        }

        public class AspNetUsersGetItemHandler : IRequestHandler<AspNetUsersGetItemQuery, Response<AspNetUsersDto>>
        {
            private readonly ILogger<AspNetUsersGetItemHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersGetItemHandler(ILogger<AspNetUsersGetItemHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUsersDto>> Handle(AspNetUsersGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}