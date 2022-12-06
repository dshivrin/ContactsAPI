using ContactsAPI.Data.Models;
using ContactsAPI.Models;
using ContactsAPI.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly IContactService contactService;

        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }

        [HttpGet("{skip}/{take}")]
        public async Task<ActionResult<PagedResult>> GetPagedContacts(int skip, int take)
        {
            var result = await contactService.GetPaged(skip, take);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await contactService.GetContactByIDAsync(id);
            if (contact?.Id == 0) return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> AddContact([FromBody] Contact contact)
        {
            var result = await contactService.AddAsync(contact);
            if (result is null || result.Id == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went terribly wrong while adding new contact");
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<Contact>> UpdateContact([FromBody] Contact request)
        {
            var result = await contactService.UpdateAsync(request);
            if (result is null || result.Id == 0) return BadRequest($"Cound not update contact {request.Id} since it's not found");
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<List<Contact>>> DeleteContact([FromBody] Contact request)
        {
            var result = await contactService.DeleteAsync(request);
            if (result is null || result.Id == 0) return StatusCode(StatusCodes.Status500InternalServerError, $"Could not delete contact {request.Id}");
            return Ok(result);
        }

    }
}
