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
    public class AspNetUserLoginsGetItemQuery : IRequest<Response<AspNetUserLoginsDto>>
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }

        public AspNetUserLoginsGetItemQuery(string loginProvider, string providerKey)
        {
            this.LoginProvider = loginProvider;
            this.ProviderKey = providerKey;
        }

        public class AspNetUserLoginsGetItemHandler : IRequestHandler<AspNetUserLoginsGetItemQuery, Response<AspNetUserLoginsDto>>
        {
            private readonly ILogger<AspNetUserLoginsGetItemHandler> _logger;
            private readonly IAspNetUserLoginsRepository _repository;
            public AspNetUserLoginsGetItemHandler(ILogger<AspNetUserLoginsGetItemHandler> logger, IAspNetUserLoginsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<AspNetUserLoginsDto>> Handle(AspNetUserLoginsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.LoginProvider, request.ProviderKey);
                return returnValue.ToResponse();
            }
        }
    }
}