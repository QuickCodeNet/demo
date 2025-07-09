using System.Linq;
using QuickCode.Demo.Common.Mediator;
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
    public class OtpTypesDeleteCommand : IRequest<Response<bool>>
    {
        public OtpTypesDto request { get; set; }

        public OtpTypesDeleteCommand(OtpTypesDto request)
        {
            this.request = request;
        }

        public class OtpTypesDeleteHandler : IRequestHandler<OtpTypesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<OtpTypesDeleteHandler> _logger;
            private readonly IOtpTypesRepository _repository;
            public OtpTypesDeleteHandler(ILogger<OtpTypesDeleteHandler> logger, IOtpTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(OtpTypesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}