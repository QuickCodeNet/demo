using System.Linq;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos;

namespace QuickCode.Demo.SmsManagerModule.Application.Features
{
    public class OtpTypesGetItemQuery : IRequest<Response<OtpTypesDto>>
    {
        public int Id { get; set; }

        public OtpTypesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class OtpTypesGetItemHandler : IRequestHandler<OtpTypesGetItemQuery, Response<OtpTypesDto>>
        {
            private readonly ILogger<OtpTypesGetItemHandler> _logger;
            private readonly IOtpTypesRepository _repository;
            public OtpTypesGetItemHandler(ILogger<OtpTypesGetItemHandler> logger, IOtpTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpTypesDto>> Handle(OtpTypesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}