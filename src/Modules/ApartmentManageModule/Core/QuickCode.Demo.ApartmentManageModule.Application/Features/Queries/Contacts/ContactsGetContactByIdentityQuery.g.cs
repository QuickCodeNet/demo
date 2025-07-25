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
    public class ContactsGetContactByIdentityQuery : IRequest<Response<ContactsGetContactByIdentityResponseDto>>
    {
        public string? ContactsIdentityNumber { get; set; }

        public ContactsGetContactByIdentityQuery(string? contactsIdentityNumber)
        {
            this.ContactsIdentityNumber = contactsIdentityNumber;
        }

        public class ContactsGetContactByIdentityHandler : IRequestHandler<ContactsGetContactByIdentityQuery, Response<ContactsGetContactByIdentityResponseDto>>
        {
            private readonly ILogger<ContactsGetContactByIdentityHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetContactByIdentityHandler(ILogger<ContactsGetContactByIdentityHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ContactsGetContactByIdentityResponseDto>> Handle(ContactsGetContactByIdentityQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetContactByIdentityAsync(request.ContactsIdentityNumber);
                return returnValue.ToResponse();
            }
        }
    }
}