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
    public class OtpMessagesGetItemQuery : IRequest<Response<OtpMessagesDto>>
    {
        public int Id { get; set; }

        public OtpMessagesGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class OtpMessagesGetItemHandler : IRequestHandler<OtpMessagesGetItemQuery, Response<OtpMessagesDto>>
        {
            private readonly ILogger<OtpMessagesGetItemHandler> _logger;
            private readonly IOtpMessagesRepository _repository;
            public OtpMessagesGetItemHandler(ILogger<OtpMessagesGetItemHandler> logger, IOtpMessagesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessagesDto>> Handle(OtpMessagesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}