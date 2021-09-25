using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerEventManagementAPI.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerEventManagementAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventsRepository eventsRepository;

        public EventsController(IEventsRepository eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        [HttpGet]
        [Route("{eventId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        public IActionResult GetById(int eventId)
        {
            var askedEvent = eventsRepository.GetById(eventId);
            return askedEvent != null ? Ok(askedEvent) : NotFound();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Event>))]
        public IActionResult GetAll() => Ok(eventsRepository.GetAll());

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] Event newEvent)
        {
            if (newEvent.Id < 1) return BadRequest("Invalid Id");
            return Created("", eventsRepository.Add(newEvent));
        }

        [HttpDelete]
        [Route("{eventId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type=typeof(string))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int eventId)
        {
            try
            {
                eventsRepository.Delete(eventId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
