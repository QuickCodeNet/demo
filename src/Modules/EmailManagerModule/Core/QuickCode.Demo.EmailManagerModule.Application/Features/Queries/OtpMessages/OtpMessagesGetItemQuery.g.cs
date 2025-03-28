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
            private readonly IMapper _mapper;
            private readonly IOtpMessagesRepository _repository;
            public OtpMessagesGetItemHandler(IMapper mapper, ILogger<OtpMessagesGetItemHandler> logger, IOtpMessagesRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<OtpMessagesDto>> Handle(OtpMessagesGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = _mapper.Map<Response<OtpMessagesDto>>(await _repository.GetByPkAsync(request.Id));
                return returnValue;
            }
        }
    }
}