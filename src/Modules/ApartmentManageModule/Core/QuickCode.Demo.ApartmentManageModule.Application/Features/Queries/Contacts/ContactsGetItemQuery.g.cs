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
    public class ContactsGetItemQuery : IRequest<Response<ContactsDto>>
    {
        public int Id { get; set; }

        public ContactsGetItemQuery(int id)
        {
            this.Id = id;
        }

        public class ContactsGetItemHandler : IRequestHandler<ContactsGetItemQuery, Response<ContactsDto>>
        {
            private readonly ILogger<ContactsGetItemHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetItemHandler(ILogger<ContactsGetItemHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ContactsDto>> Handle(ContactsGetItemQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.GetByPkAsync(request.Id);
                return returnValue.ToResponse();
            }
        }
    }
}