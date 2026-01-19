using System;
using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.SmsManagerModule.Domain.Entities;
using QuickCode.Demo.SmsManagerModule.Application.Interfaces.Repositories;
using QuickCode.Demo.SmsManagerModule.Application.Dtos.MessageTemplate;
using QuickCode.Demo.SmsManagerModule.Domain.Enums;

namespace QuickCode.Demo.SmsManagerModule.Application.Features.MessageTemplate
{
    public class ListMessageTemplateQuery : IRequest<Response<List<MessageTemplateDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public ListMessageTemplateQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class ListMessageTemplateHandler : IRequestHandler<ListMessageTemplateQuery, Response<List<MessageTemplateDto>>>
        {
            private readonly ILogger<ListMessageTemplateHandler> _logger;
            private readonly IMessageTemplateRepository _repository;
            public ListMessageTemplateHandler(ILogger<ListMessageTemplateHandler> logger, IMessageTemplateRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<MessageTemplateDto>>> Handle(ListMessageTemplateQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}