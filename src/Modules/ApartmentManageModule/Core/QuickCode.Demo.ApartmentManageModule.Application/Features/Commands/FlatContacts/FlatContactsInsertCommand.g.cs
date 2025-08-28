using System.Linq;
using QuickCode.Demo.Common.Mediator;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.ApartmentManageModule.Domain.Entities;
using QuickCode.Demo.ApartmentManageModule.Application.Interfaces.Repositories;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Application.Features
{
    public class FlatContactsInsertCommand : IRequest<Response<FlatContactsDto>>
    {
        public FlatContactsDto request { get; set; }

        public FlatContactsInsertCommand(FlatContactsDto request)
        {
            this.request = request;
        }

        public class FlatContactsInsertHandler : IRequestHandler<FlatContactsInsertCommand, Response<FlatContactsDto>>
        {
            private readonly ILogger<FlatContactsInsertHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsInsertHandler(ILogger<FlatContactsInsertHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatContactsDto>> Handle(FlatContactsInsertCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.InsertAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}