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
    public class ContactsTotalItemCountQuery : IRequest<Response<int>>
    {
        public ContactsTotalItemCountQuery()
        {
        }

        public class ContactsTotalItemCountHandler : IRequestHandler<ContactsTotalItemCountQuery, Response<int>>
        {
            private readonly ILogger<ContactsTotalItemCountHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsTotalItemCountHandler(ILogger<ContactsTotalItemCountHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<int>> Handle(ContactsTotalItemCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.CountAsync();
                return returnValue.ToResponse();
            }
        }
    }
}