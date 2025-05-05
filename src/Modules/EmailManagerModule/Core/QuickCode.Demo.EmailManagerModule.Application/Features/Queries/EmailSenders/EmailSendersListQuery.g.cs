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
    public class EmailSendersListQuery : IRequest<Response<List<EmailSendersDto>>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public EmailSendersListQuery(int? pageNumber, int? pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public class EmailSendersListHandler : IRequestHandler<EmailSendersListQuery, Response<List<EmailSendersDto>>>
        {
            private readonly ILogger<EmailSendersListHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersListHandler(ILogger<EmailSendersListHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<EmailSendersDto>>> Handle(EmailSendersListQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ListAsync(request.PageNumber, request.PageSize);
                return returnValue.ToResponse();
            }
        }
    }
}