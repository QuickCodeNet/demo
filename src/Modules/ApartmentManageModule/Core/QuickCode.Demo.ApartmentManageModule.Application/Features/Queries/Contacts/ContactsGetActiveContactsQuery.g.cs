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
    public class ContactsGetActiveContactsQuery : IRequest<Response<List<ContactsGetActiveContactsResponseDto>>>
    {
        public bool ContactsIsActive { get; set; }

        public ContactsGetActiveContactsQuery(bool contactsIsActive)
        {
            this.ContactsIsActive = contactsIsActive;
        }

        public class ContactsGetActiveContactsHandler : IRequestHandler<ContactsGetActiveContactsQuery, Response<List<ContactsGetActiveContactsResponseDto>>>
        {
            private readonly ILogger<ContactsGetActiveContactsHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetActiveContactsHandler(ILogger<ContactsGetActiveContactsHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ContactsGetActiveContactsResponseDto>>> Handle(ContactsGetActiveContactsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetActiveContactsAsync(request.ContactsIsActive);
                return returnValue.ToResponse();
            }
        }
    }
}