using System;
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
    public class ContactsGetActiveContactsCountQuery : IRequest<Response<long>>
    {
        public bool ContactsIsActive { get; set; }

        public ContactsGetActiveContactsCountQuery(bool contactsIsActive)
        {
            this.ContactsIsActive = contactsIsActive;
        }

        public class ContactsGetActiveContactsCountHandler : IRequestHandler<ContactsGetActiveContactsCountQuery, Response<long>>
        {
            private readonly ILogger<ContactsGetActiveContactsCountHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetActiveContactsCountHandler(ILogger<ContactsGetActiveContactsCountHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<long>> Handle(ContactsGetActiveContactsCountQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetActiveContactsCountAsync(request.ContactsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}