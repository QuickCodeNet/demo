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
    public class AspNetUserClaimsDeleteItemCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }

        public AspNetUserClaimsDeleteItemCommand(int id)
        {
            this.Id = id;
        }

        public class AspNetUserClaimsDeleteItemHandler : IRequestHandler<AspNetUserClaimsDeleteItemCommand, Response<bool>>
        {
            private readonly ILogger<AspNetUserClaimsDeleteItemHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IAspNetUserClaimsRepository _repository;
            public AspNetUserClaimsDeleteItemHandler(IMapper mapper, ILogger<AspNetUserClaimsDeleteItemHandler> logger, IAspNetUserClaimsRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(AspNetUserClaimsDeleteItemCommand request, CancellationToken cancellationToken)
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

                var returnValue = _mapper.Map<Response<bool>>(await _repository.DeleteAsync(deleteItem.Value));
                return returnValue;
            }
        }
    }
}