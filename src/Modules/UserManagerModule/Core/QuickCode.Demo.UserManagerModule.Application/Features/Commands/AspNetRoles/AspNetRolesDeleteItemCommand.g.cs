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
    public class AspNetRolesDeleteItemCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; }

        public AspNetRolesDeleteItemCommand(string id)
        {
            this.Id = id;
        }

        public class AspNetRolesDeleteItemHandler : IRequestHandler<AspNetRolesDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<AspNetRolesDeleteItemHandler> _logger;
            private readonly IAspNetRolesRepository _repository;
            public AspNetRolesDeleteItemHandler(ILogger<AspNetRolesDeleteItemHandler> logger, IAspNetRolesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetRolesDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Id);
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