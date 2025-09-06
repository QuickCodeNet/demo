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
    public class EmailSendersGetItemQuery : IRequest<Response<EmailSendersDto>>
    {
        public int Id { get; set; }

        public EmailSendersGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class EmailSendersGetItemHandler : IRequestHandler<EmailSendersGetItemQuery, Response<EmailSendersDto>>
        {
            private readonly ILogger<EmailSendersGetItemHandler> _logger;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersGetItemHandler(ILogger<EmailSendersGetItemHandler> logger, IEmailSendersRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<EmailSendersDto>> Handle(EmailSendersGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}