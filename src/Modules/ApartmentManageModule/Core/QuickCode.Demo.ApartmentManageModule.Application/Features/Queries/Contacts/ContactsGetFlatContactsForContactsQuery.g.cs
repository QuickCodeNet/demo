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
    public class ContactsGetFlatContactsForContactsQuery : IRequest<Response<List<ContactsGetFlatContactsForContactsResponseDto>>>
    {
        public int ContactsId { get; set; }

        public ContactsGetFlatContactsForContactsQuery(int contactsId)
        {
            this.ContactsId = contactsId;
        }

        public class ContactsGetFlatContactsForContactsHandler : IRequestHandler<ContactsGetFlatContactsForContactsQuery, Response<List<ContactsGetFlatContactsForContactsResponseDto>>>
        {
            private readonly ILogger<ContactsGetFlatContactsForContactsHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetFlatContactsForContactsHandler(ILogger<ContactsGetFlatContactsForContactsHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ContactsGetFlatContactsForContactsResponseDto>>> Handle(ContactsGetFlatContactsForContactsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetFlatContactsForContactsAsync(request.ContactsId);
                return returnValue.ToResponse();
            }
        }
    }
}