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
    public class AspNetUserClaimsUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public AspNetUserClaimsDto request { get; set; }

        public AspNetUserClaimsUpdateCommand(int id, AspNetUserClaimsDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class AspNetUserClaimsUpdateHandler : IRequestHandler<AspNetUserClaimsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserClaimsUpdateHandler> _logger;
            private readonly IAspNetUserClaimsRepository _repository;
            public AspNetUserClaimsUpdateHandler(ILogger<AspNetUserClaimsUpdateHandler> logger, IAspNetUserClaimsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserClaimsUpdateCommand request, CancellationToken cancellationToken)
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