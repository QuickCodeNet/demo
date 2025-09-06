using System;
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
    public class OtpTypesGetOtpMessagesForOtpTypesDetailsQuery : IRequest<Response<OtpTypesGetOtpMessagesForOtpTypesResponseDto>>
    {
        public int OtpTypesId { get; set; }
        public int OtpMessagesId { get; set; }

        public OtpTypesGetOtpMessagesForOtpTypesDetailsQuery(int otpTypesId, int otpMessagesId)
        {
            this.OtpTypesId = otpTypesId;
            this.OtpMessagesId = otpMessagesId;
        }

        public class OtpTypesGetOtpMessagesForOtpTypesDetailsHandler : IRequestHandler<OtpTypesGetOtpMessagesForOtpTypesDetailsQuery, Response<OtpTypesGetOtpMessagesForOtpTypesResponseDto>>
        {
            private readonly ILogger<OtpTypesGetOtpMessagesForOtpTypesDetailsHandler> _logger;
            private readonly IOtpTypesRepository _repository;
            public OtpTypesGetOtpMessagesForOtpTypesDetailsHandler(ILogger<OtpTypesGetOtpMessagesForOtpTypesDetailsHandler> logger, IOtpTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpTypesGetOtpMessagesForOtpTypesResponseDto>> Handle(OtpTypesGetOtpMessagesForOtpTypesDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.OtpTypesGetOtpMessagesForOtpTypesDetailsAsync(request.OtpTypesId, request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}