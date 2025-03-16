using AutoMapper;
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
    public class EmailSendersTotalItemCountQuery : IRequest<Response<int>>
    {
        public EmailSendersTotalItemCountQuery()
        {
        }

        public class EmailSendersTotalItemCountHandler : IRequestHandler<EmailSendersTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<EmailSendersTotalItemCountHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IEmailSendersRepository _repository;
            public EmailSendersTotalItemCountHandler(IMapper mapper, ILogger<EmailSendersTotalItemCountHandler> logger, IEmailSendersRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(EmailSendersTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = _mapper.Map<Response<int>>(await _repository.CountAsync());
                return returnValue;
            }
        }
    }
}