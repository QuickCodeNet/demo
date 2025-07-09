using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.EmailManagerModule.Domain.Entities;
using QuickCode.Demo.EmailManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.EmailManagerModule.Application.Dtos;

namespace QuickCode.Demo.EmailManagerModule.Application.Features
{
    public class OtpTypesInsertCommand : IRequest<Response<OtpTypesDto>>
    {
        public OtpTypesDto request { get; set; }

        public OtpTypesInsertCommand(OtpTypesDto request)
        {
            this.request = request;
        }

        public class OtpTypesInsertHandler : IRequestHandler<OtpTypesInsertCommand, Response<OtpTypesDto>>
        {
            private readonly ILogger<OtpTypesInsertHandler> _logger;
            private readonly IOtpTypesRepository _repository;
            public OtpTypesInsertHandler(ILogger<OtpTypesInsertHandler> logger, IOtpTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpTypesDto>> Handle(OtpTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}