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
    public class ApartmentFeePlansGetItemQuery : IRequest<Response<ApartmentFeePlansDto>>
    {
        public int Id { get; set; }

        public ApartmentFeePlansGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class ApartmentFeePlansGetItemHandler : IRequestHandler<ApartmentFeePlansGetItemQuery, Response<ApartmentFeePlansDto>>
        {
            private readonly ILogger<ApartmentFeePlansGetItemHandler> _logger;
            private readonly IApartmentFeePlansRepository _repository;
            public ApartmentFeePlansGetItemHandler(ILogger<ApartmentFeePlansGetItemHandler> logger, IApartmentFeePlansRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ApartmentFeePlansDto>> Handle(ApartmentFeePlansGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}