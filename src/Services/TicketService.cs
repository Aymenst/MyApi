using MyApi.Models;
using MyApi.Repositories;
using MongoDB.Bson;

namespace MyApi.Services
{
    public class TicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public Task<List<Ticket>> GetTicketsAsync(int page, int pageSize)
        {
            return _ticketRepository.GetTicketsAsync(page, pageSize);
        }

        public Task<Ticket> GetTicketByIdAsync(ObjectId id)
        {
            return _ticketRepository.GetTicketByIdAsync(id);
        }

        public Task CreateTicketAsync(Ticket ticket)
        {
            ticket.CreatedDate = DateTime.Now;
            return _ticketRepository.CreateTicketAsync(ticket);
        }

        public Task UpdateTicketAsync(Ticket ticket)
        {
            return _ticketRepository.UpdateTicketAsync(ticket);
        }

        public Task DeleteTicketAsync(ObjectId id)
        {
            return _ticketRepository.DeleteTicketAsync(id);
        }

        public Task<long> GetTicketCountAsync()
        {
            return _ticketRepository.GetTicketCountAsync();
        }
    }
}