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
    public class AspNetRoleClaimsDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public AspNetRoleClaimsDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class AspNetRoleClaimsDeleteItemHandler : IRequestHandler<AspNetRoleClaimsDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<AspNetRoleClaimsDeleteItemHandler> _logger;
            private readonly IAspNetRoleClaimsRepository _repository;
            public AspNetRoleClaimsDeleteItemHandler(ILogger<AspNetRoleClaimsDeleteItemHandler> logger, IAspNetRoleClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetRoleClaimsDeleteItemCommand request, CancellationToken cancellationToken)
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