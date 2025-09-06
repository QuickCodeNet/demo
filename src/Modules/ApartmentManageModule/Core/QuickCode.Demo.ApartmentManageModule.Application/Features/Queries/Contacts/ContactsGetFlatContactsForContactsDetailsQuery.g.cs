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
    public class ContactsGetFlatContactsForContactsDetailsQuery : IRequest<Response<ContactsGetFlatContactsForContactsResponseDto>>
    {
        public int ContactsId { get; set; }
        public int FlatContactsId { get; set; }

        public ContactsGetFlatContactsForContactsDetailsQuery(int contactsId, int flatContactsId)
        {
            this.ContactsId = contactsId;
            this.FlatContactsId = flatContactsId;
        }

        public class ContactsGetFlatContactsForContactsDetailsHandler : IRequestHandler<ContactsGetFlatContactsForContactsDetailsQuery, Response<ContactsGetFlatContactsForContactsResponseDto>>
        {
            private readonly ILogger<ContactsGetFlatContactsForContactsDetailsHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetFlatContactsForContactsDetailsHandler(ILogger<ContactsGetFlatContactsForContactsDetailsHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ContactsGetFlatContactsForContactsResponseDto>> Handle(ContactsGetFlatContactsForContactsDetailsQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetFlatContactsForContactsDetailsAsync(request.ContactsId, request.FlatContactsId);
                return returnValue.ToResponse();
            }
        }
    }
}