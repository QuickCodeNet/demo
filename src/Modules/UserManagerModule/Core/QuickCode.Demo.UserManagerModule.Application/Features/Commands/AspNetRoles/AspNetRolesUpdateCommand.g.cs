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
    public class AspNetRolesUpdateCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }
        public AspNetRolesDto request { get; set; }

        public AspNetRolesUpdateCommand(string id, AspNetRolesDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class AspNetRolesUpdateHandler : IRequestHandler<AspNetRolesUpdateCommand, Response<bool>>
        {
            private readonly ILogger<AspNetRolesUpdateHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesUpdateHandler(ILogger<AspNetRolesUpdateHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetRolesUpdateCommand request, CancellationToken cancellationToken)
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