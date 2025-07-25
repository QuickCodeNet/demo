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
    public class FlatContactsDeleteCommand : IRequest<Response<bool>>
    {
        public FlatContactsDto request { get; set; }

        public FlatContactsDeleteCommand(FlatContactsDto request)
        {
            this.request = request;
        }

        public class FlatContactsDeleteHandler : IRequestHandler<FlatContactsDeleteCommand, Response<bool>>
        {
            private readonly ILogger<FlatContactsDeleteHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsDeleteHandler(ILogger<FlatContactsDeleteHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(FlatContactsDeleteCommand request, CancellationToken cancellationToken)
            {
                var model = request.request;
                var returnValue = await _repository.DeleteAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}