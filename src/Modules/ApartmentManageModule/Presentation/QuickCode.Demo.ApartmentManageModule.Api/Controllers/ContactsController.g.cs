using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using QuickCode.Demo.Common.Controllers;
using QuickCode.Demo.ApartmentManageModule.Application.Dtos.Contact;
using QuickCode.Demo.ApartmentManageModule.Application.Services.Contact;
using QuickCode.Demo.ApartmentManageModule.Domain.Enums;

namespace QuickCode.Demo.ApartmentManageModule.Api.Controllers
{
    public partial class ContactsController : QuickCodeBaseApiController
    {
        private readonly IContactService service;
        private readonly ILogger<ContactsController> logger;
        private readonly IServiceProvider serviceProvider;
        public ContactsController(IContactService service, IServiceProvider serviceProvider, ILogger<ContactsController> logger)
        {
            this.service = service;
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ListAsync([FromQuery] int? page, int? size)
        {
            if (ValidatePagination(page, size) is {} error)
                return error;
            var response = await service.ListAsync(page, size);
            if (HandleResponseError(response, logger, "Contact", "List") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CountAsync()
        {
            var response = await service.TotalItemCountAsync();
            if (HandleResponseError(response, logger, "Contact") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetItemAsync(int id)
        {
            var response = await service.GetItemAsync(id);
            if (HandleResponseError(response, logger, "Contact", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ContactDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> InsertAsync(ContactDto model)
        {
            var response = await service.InsertAsync(model);
            if (HandleResponseError(response, logger, "Contact") is {} responseError)
                return responseError;
            return CreatedAtRoute(new { id = response.Value.Id }, response.Value);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateAsync(int id, ContactDto model)
        {
            if (!(model.Id == id))
            {
                return BadRequest($"Id: '{id}' must be equal to model.Id: '{model.Id}'");
            }

            var response = await service.UpdateAsync(id, model);
            if (HandleResponseError(response, logger, "Contact", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await service.DeleteItemAsync(id);
            if (HandleResponseError(response, logger, "Contact", $"Id: '{id}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-contacts/{contactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetActiveContactsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveContactsAsync(bool contactsIsActive)
        {
            var response = await service.GetActiveContactsAsync(contactsIsActive);
            if (HandleResponseError(response, logger, "Contact", $"ContactsIsActive: '{contactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-contact-by-id/{contactsId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetContactByIdResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactByIdAsync(int contactsId)
        {
            var response = await service.GetContactByIdAsync(contactsId);
            if (HandleResponseError(response, logger, "Contact", $"ContactsId: '{contactsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-contact-by-phone/{contactsPhone}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetContactByPhoneResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactByPhoneAsync(string contactsPhone)
        {
            var response = await service.GetContactByPhoneAsync(contactsPhone);
            if (HandleResponseError(response, logger, "Contact", $"ContactsPhone: '{contactsPhone}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-contact-by-email/{contactsEmail}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetContactByEmailResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactByEmailAsync(string contactsEmail)
        {
            var response = await service.GetContactByEmailAsync(contactsEmail);
            if (HandleResponseError(response, logger, "Contact", $"ContactsEmail: '{contactsEmail}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-contact-by-identity/{contactsIdentityNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetContactByIdentityResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactByIdentityAsync(string contactsIdentityNumber)
        {
            var response = await service.GetContactByIdentityAsync(contactsIdentityNumber);
            if (HandleResponseError(response, logger, "Contact", $"ContactsIdentityNumber: '{contactsIdentityNumber}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("check-contact-by-phone/{contactsPhone}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CheckContactByPhoneAsync(string contactsPhone)
        {
            var response = await service.CheckContactByPhoneAsync(contactsPhone);
            if (HandleResponseError(response, logger, "Contact", $"ContactsPhone: '{contactsPhone}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("check-contact-by-email/{contactsEmail}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CheckContactByEmailAsync(string contactsEmail)
        {
            var response = await service.CheckContactByEmailAsync(contactsEmail);
            if (HandleResponseError(response, logger, "Contact", $"ContactsEmail: '{contactsEmail}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-active-contacts-count/{contactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(long))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetActiveContactsCountAsync(bool contactsIsActive)
        {
            var response = await service.GetActiveContactsCountAsync(contactsIsActive);
            if (HandleResponseError(response, logger, "Contact", $"ContactsIsActive: '{contactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("get-contacts-with-pager/{contactsIsActive:bool}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetContactsWithPagerResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetContactsWithPagerAsync(bool contactsIsActive, int? page, int? size)
        {
            if (page < 1)
            {
                var pageNumberError = $"Page Number must be greater than 1";
                logger.LogWarning($"List Error: '{pageNumberError}''");
                return NotFound(pageNumberError);
            }

            var response = await service.GetContactsWithPagerAsync(contactsIsActive, page, size);
            if (HandleResponseError(response, logger, "Contact", $"ContactsIsActive: '{contactsIsActive}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{contactId}/flat-contact")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetFlatContactsForContactsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatContactsForContactsAsync(int contactsId)
        {
            var response = await service.GetFlatContactsForContactsAsync(contactsId);
            if (HandleResponseError(response, logger, "Contact", $"ContactsId: '{contactsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{contactId}/flat-contact/{flatContactId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetFlatContactsForContactsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetFlatContactsForContactsDetailsAsync(int contactsId, int flatContactsId)
        {
            var response = await service.GetFlatContactsForContactsDetailsAsync(contactsId, flatContactsId);
            if (HandleResponseError(response, logger, "Contact", $"ContactsId: '{contactsId}', FlatContactsId: '{flatContactsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{contactId}/site-manager")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GetSiteManagersForContactsResponseDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteManagersForContactsAsync(int contactsId)
        {
            var response = await service.GetSiteManagersForContactsAsync(contactsId);
            if (HandleResponseError(response, logger, "Contact", $"ContactsId: '{contactsId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }

        [HttpGet("{contactId}/site-manager/{siteManagerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSiteManagersForContactsResponseDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> GetSiteManagersForContactsDetailsAsync(int contactsId, int siteManagersId)
        {
            var response = await service.GetSiteManagersForContactsDetailsAsync(contactsId, siteManagersId);
            if (HandleResponseError(response, logger, "Contact", $"ContactsId: '{contactsId}', SiteManagersId: '{siteManagersId}'") is {} responseError)
                return responseError;
            return Ok(response.Value);
        }
    }
}