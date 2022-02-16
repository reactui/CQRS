using Invelop.CQRS.WebApi.Application.Features.ContactFeatures.Commands;
using Invelop.CQRS.WebApi.Application.Features.ContactFeatures.Queries;
using Invelop.CQRS.WebApi.Features.ContactFeatures.Commands;
using Invelop.CQRS.WebApi.Infrastructure.Features.ContactFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Invelop.CQRS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        /// <summary>
        /// Creates a New Contact.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateContactCommand command)
        {            
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (FluentValidation.ValidationException validationException)
            {                
                return BadRequest(validationException.Message);
            }
        }
        /// <summary>
        /// Gets all Contacts.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllContactsQuery()));
        }


        /// <summary>
        /// Gets Contact Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetContactByIdQuery { Id = id }));
        }
        /// <summary>
        /// Deletes Contact Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteContactByIdCommand { Id = id }));
        }
        /// <summary>
        /// Updates the Contact Entity based on Id.   
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// 


        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContactCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Cannot update record.");
            }

            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (FluentValidation.ValidationException validationException)
            {
                return BadRequest(validationException.Message);
            }
        }
    }
}