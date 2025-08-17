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
    public class FlatContactsGetItemQuery : IRequest<Response<FlatContactsDto>>
    {
        public int Id { get; set; }

        public FlatContactsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class FlatContactsGetItemHandler : IRequestHandler<FlatContactsGetItemQuery, Response<FlatContactsDto>>
        {
            private readonly ILogger<FlatContactsGetItemHandler> _logger;
            private readonly IFlatContactsRepository _repository;
            public FlatContactsGetItemHandler(ILogger<FlatContactsGetItemHandler> logger, IFlatContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<FlatContactsDto>> Handle(FlatContactsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}