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
    public class AspNetUsersUpdateCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public AspNetUsersDto request { get; set; }

        public AspNetUsersUpdateCommand(string id, AspNetUsersDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class AspNetUsersUpdateHandler : IRequestHandler<AspNetUsersUpdateCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUsersUpdateHandler> _logger;
            private readonly IAspNetUsersRepository _repository;
            public AspNetUsersUpdateHandler(ILogger<AspNetUsersUpdateHandler> logger, IAspNetUsersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUsersUpdateCommand request, CancellationToken cancellationToken)
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