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
    public class InfoTypesDeleteCommand : IRequest<Response<bool>>
    {
        public InfoTypesDto request { get; set; }

        public InfoTypesDeleteCommand(InfoTypesDto request)
        {
            this.request = request;
        }

        public class InfoTypesDeleteHandler : IRequestHandler<InfoTypesDeleteCommand, Response<bool>>
        {
            private readonly ILogger<InfoTypesDeleteHandler> _logger;
            private readonly IInfoTypesRepository _repository;
            public InfoTypesDeleteHandler(ILogger<InfoTypesDeleteHandler> logger, IInfoTypesRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(InfoTypesDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}