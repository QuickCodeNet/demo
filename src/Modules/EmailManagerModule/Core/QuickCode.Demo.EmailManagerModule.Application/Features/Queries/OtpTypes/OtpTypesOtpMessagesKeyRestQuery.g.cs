using System;
using System.Linq;
using MediatR;
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
    public class OtpTypesOtpTypesOtpMessagesKeyRestQuery : IRequest<Response<OtpTypesOtpMessagesKeyRestResponseDto>>
    {
        public int OtpTypesId { get; set; }
        public int OtpMessagesId { get; set; }

        public OtpTypesOtpTypesOtpMessagesKeyRestQuery(int otpTypesId, int otpMessagesId)
        {
            this.OtpTypesId = otpTypesId;
            this.OtpMessagesId = otpMessagesId;
        }

        public class OtpTypesOtpTypesOtpMessagesKeyRestHandler : IRequestHandler<OtpTypesOtpTypesOtpMessagesKeyRestQuery, Response<OtpTypesOtpMessagesKeyRestResponseDto>>
        {
            private readonly ILogger<OtpTypesOtpTypesOtpMessagesKeyRestHandler> _logger;
            private readonly IOtpTypesRepository _repository;
            public OtpTypesOtpTypesOtpMessagesKeyRestHandler(ILogger<OtpTypesOtpTypesOtpMessagesKeyRestHandler> logger, IOtpTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpTypesOtpMessagesKeyRestResponseDto>> Handle(OtpTypesOtpTypesOtpMessagesKeyRestQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.OtpTypesOtpMessagesKeyRestAsync(request.OtpTypesId, request.OtpMessagesId);
                return returnValue.ToResponse();
            }
        }
    }
}