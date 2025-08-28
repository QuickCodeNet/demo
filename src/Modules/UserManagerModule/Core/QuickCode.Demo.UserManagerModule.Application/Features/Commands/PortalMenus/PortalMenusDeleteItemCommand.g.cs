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
    public class PortalMenusDeleteItemCommand : IRequest<Response<bool>>
    {
        public string Key { get; set; }

        public PortalMenusDeleteItemCommand(string key)
        {
            this.Key = key;
        }

        public class PortalMenusDeleteItemHandler : IRequestHandler<PortalMenusDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<PortalMenusDeleteItemHandler> _logger;
            private readonly IPortalMenusRepository _repository;
            public PortalMenusDeleteItemHandler(ILogger<PortalMenusDeleteItemHandler> logger, IPortalMenusRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(PortalMenusDeleteItemCommand request, CancellationToken cancellationToken)
            {
                var deleteItem = await _repository.GetByPkAsync(request.Key);
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