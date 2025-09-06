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
    public class ContactsGetContactByIdQuery : IRequest<Response<ContactsGetContactByIdResponseDto>>
    {
        public int ContactsId { get; set; }

        public ContactsGetContactByIdQuery(int contactsId)
        {
            this.ContactsId = contactsId;
        }

        public class ContactsGetContactByIdHandler : IRequestHandler<ContactsGetContactByIdQuery, Response<ContactsGetContactByIdResponseDto>>
        {
            private readonly ILogger<ContactsGetContactByIdHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetContactByIdHandler(ILogger<ContactsGetContactByIdHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<ContactsGetContactByIdResponseDto>> Handle(ContactsGetContactByIdQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetContactByIdAsync(request.ContactsId);
                return returnValue.ToResponse();
            }
        }
    }
}