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
    public class ContactsUpdateCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
        public ContactsDto request { get; set; }

        public ContactsUpdateCommand(int id, ContactsDto request)
        {
            this.request = request;
            this.Id = id;
        }

        public class ContactsUpdateHandler : IRequestHandler<ContactsUpdateCommand, Response<bool>>
        {
            private readonly ILogger<ContactsUpdateHandler> _logger;
            private readonly IContactsRepository _repository;
            public ContactsUpdateHandler(ILogger<ContactsUpdateHandler> logger, IContactsRepository repository)
            {
                _logger = logger;
                _repository = repository;
            }

            public async Task<Response<bool>> Handle(ContactsUpdateCommand request, CancellationToken cancellationToken)
            {
                var updateItem = await _repository.GetByPkAsync(request.Id);
                if (updateItem.Code == 404)
                {
                    return new Response<bool>()
                    {
                        Code = 404,
                        Value = false
                    };
                }

                var model = request.request;
                var returnValue = await _repository.UpdateAsync(model);
                return returnValue.ToResponse();
            }
        }
    }
}