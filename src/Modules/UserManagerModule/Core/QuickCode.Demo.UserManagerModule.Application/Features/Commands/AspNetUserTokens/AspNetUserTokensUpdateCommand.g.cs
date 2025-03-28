using AutoMapper;
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
    public class AspNetUserTokensUpdateCommand : IRequest<Response<bool>>
    {
        public string UserId { get; set; }
        public AspNetUserTokensDto request { get; set; }

        public AspNetUserTokensUpdateCommand(string userId, AspNetUserTokensDto request)
        {
            this.request = request;
            this.UserId = userId;
        }

        public class AspNetUserTokensUpdateHandler : IRequestHandler<AspNetUserTokensUpdateCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserTokensUpdateHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IAspNetUserTokensRepository _repository;
            public AspNetUserTokensUpdateHandler(IMapper mapper, ILogger<AspNetUserTokensUpdateHandler> logger, IAspNetUserTokensRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserTokensUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.UserId);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = _mapper.Map<AspNetUserTokens>(request.request);
                var returnValue = _mapper.Map<Response<bool>>(await _repository.UpdateAsync(model));
                return returnValue;
            }
        }
    }
}