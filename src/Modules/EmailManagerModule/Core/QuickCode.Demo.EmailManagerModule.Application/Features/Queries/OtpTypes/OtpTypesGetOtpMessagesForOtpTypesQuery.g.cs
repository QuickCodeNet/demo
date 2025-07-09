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
    public class OtpTypesGetOtpMessagesForOtpTypesQuery : IRequest<Response<List<OtpTypesGetOtpMessagesForOtpTypesResponseDto>>>
    {
        public int OtpTypesId { get; set; }

        public OtpTypesGetOtpMessagesForOtpTypesQuery(int otpTypesId)
        {
            this.OtpTypesId = otpTypesId;
        }

        public class OtpTypesGetOtpMessagesForOtpTypesHandler : IRequestHandler<OtpTypesGetOtpMessagesForOtpTypesQuery, Response<List<OtpTypesGetOtpMessagesForOtpTypesResponseDto>>>
        {
            private readonly ILogger<OtpTypesGetOtpMessagesForOtpTypesHandler> _logger;
            private readonly IOtpTypesRepository _repository;
            public OtpTypesGetOtpMessagesForOtpTypesHandler(ILogger<OtpTypesGetOtpMessagesForOtpTypesHandler> logger, IOtpTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<OtpTypesGetOtpMessagesForOtpTypesResponseDto>>> Handle(OtpTypesGetOtpMessagesForOtpTypesQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.OtpTypesGetOtpMessagesForOtpTypesAsync(request.OtpTypesId);
                return returnValue.ToResponse();
            }
        }
    }
}