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
            private readonly IInfoTypesRepository _repository;
            public InfoTypesInsertHandler(ILogger<InfoTypesInsertHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<InfoTypesDto>> Handle(InfoTypesInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}