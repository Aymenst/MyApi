using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MyApi.Models;
using MyApi.Services;

namespace MyApi.Controllers
{
    [Route("api/Ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets(int page = 1, int pageSize = 10)
        {
            var tickets = await _ticketService.GetTicketsAsync(page, pageSize);
            var count = await _ticketService.GetTicketCountAsync();
            return Ok(new { tickets, count });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(string id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(new ObjectId(id));
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Ticket ticket)
        {
            await _ticketService.CreateTicketAsync(ticket);
            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id.ToString() }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(string id, Ticket ticket)
        {
            if (id != ticket.Id.ToString())
            {
                return BadRequest();
            }
            await _ticketService.UpdateTicketAsync(ticket);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            await _ticketService.DeleteTicketAsync(new ObjectId(id));
            return NoContent();
        }
    }
}