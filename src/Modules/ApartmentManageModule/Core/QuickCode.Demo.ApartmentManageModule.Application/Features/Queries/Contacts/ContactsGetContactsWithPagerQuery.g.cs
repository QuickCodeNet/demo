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
    public class ContactsGetContactsWithPagerQuery : IRequest<Response<List<ContactsGetContactsWithPagerResponseDto>>>
    {
        public bool ContactsIsActive { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }

        public ContactsGetContactsWithPagerQuery(bool contactsIsActive, int? pageNumber, int? pageSize)
        {
            this.ContactsIsActive = contactsIsActive;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }

        public class ContactsGetContactsWithPagerHandler : IRequestHandler<ContactsGetContactsWithPagerQuery, Response<List<ContactsGetContactsWithPagerResponseDto>>>
        {
            private readonly ILogger<ContactsGetContactsWithPagerHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsGetContactsWithPagerHandler(ILogger<ContactsGetContactsWithPagerHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<List<ContactsGetContactsWithPagerResponseDto>>> Handle(ContactsGetContactsWithPagerQuery request, CancellationToken cancellationToken)
            {
                var returnValue = await _repository.ContactsGetContactsWithPagerAsync(request.ContactsIsActive, request.pageNumber, request.pageSize);
                return returnValue.ToResponse();
            }
        }
    }
}