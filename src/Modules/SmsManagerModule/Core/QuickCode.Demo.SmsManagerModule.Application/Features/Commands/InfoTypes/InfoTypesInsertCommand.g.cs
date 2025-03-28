using AutoMapper;
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
    public class InfoTypesInsertCommand : IRequest<Response<InfoTypesDto>>
    {
        public InfoTypesDto request { get; set; }

        public InfoTypesInsertCommand(InfoTypesDto request)
        {
            this.request = request;
        }

        public class InfoTypesInsertHandler : IRequestHandler<InfoTypesInsertCommand, Response<InfoTypesDto>>
        {
            private readonly ILogger<InfoTypesInsertHandler> _logger;
            private readonly IMapper _mapper;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesInsertHandler(IMapper mapper, ILogger<InfoTypesInsertHandler> logger, IInfoTypesRepository repository)
            {
                _mapper = mapper;
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<InfoTypesDto>> Handle(InfoTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = _mapper.Map<InfoTypes>(request.request);
                var returnValue = _mapper.Map<Response<InfoTypesDto>>(await _repository.InsertAsync(model));
                return returnValue;
            }
        }
    }
}