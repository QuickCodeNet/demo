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
    public class ContactsGetSiteManagersForContactsQuery : IRequest<Response<List<ContactsGetSiteManagersForContactsResponseDto>>>
    {
        public int ContactsId { get; set; }

        public ContactsGetSiteManagersForContactsQuery(int contactsId)
        {
            this.ContactsId = contactsId;
        }

        public class ContactsGetSiteManagersForContactsHandler : IRequestHandler<ContactsGetSiteManagersForContactsQuery, Response<List<ContactsGetSiteManagersForContactsResponseDto>>>
        {
            private readonly ILogger<ContactsGetSiteManagersForContactsHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetSiteManagersForContactsHandler(ILogger<ContactsGetSiteManagersForContactsHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ContactsGetSiteManagersForContactsResponseDto>>> Handle(ContactsGetSiteManagersForContactsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetSiteManagersForContactsAsync(request.ContactsId);
                return returnValue.ToResponse();
            }
        }
    }
}