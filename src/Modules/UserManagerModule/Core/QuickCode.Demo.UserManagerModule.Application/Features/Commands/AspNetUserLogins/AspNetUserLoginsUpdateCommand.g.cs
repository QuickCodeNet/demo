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
    public class AspNetUserLoginsUpdateCommand : IRequest<Response<bool>>
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public AspNetUserLoginsDto request { get; set; }

        public AspNetUserLoginsUpdateCommand(string loginProvider, string providerKey, AspNetUserLoginsDto request)
        {
            this.request = request;
            this.LoginProvider = loginProvider;
            this.ProviderKey = providerKey;
        }

        public class AspNetUserLoginsUpdateHandler : IRequestHandler<AspNetUserLoginsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserLoginsUpdateHandler> _logger;
            private readonly IAspNetUserLoginsRepository _repository;
            public AspNetUserLoginsUpdateHandler(ILogger<AspNetUserLoginsUpdateHandler> logger, IAspNetUserLoginsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserLoginsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.LoginProvider, request.ProviderKey);
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