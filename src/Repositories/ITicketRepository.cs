using MyApi.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MyApi.Repositories
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetTicketsAsync(int page, int pageSize);
        Task<Ticket> GetTicketByIdAsync(ObjectId id);
        Task CreateTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(ObjectId id);
        Task<long> GetTicketCountAsync();
    }

    public class TicketRepository : ITicketRepository
    {
        private readonly IMongoCollection<Ticket> _tickets;

        public TicketRepository(IMongoClient client)
        {
            var database = client.GetDatabase("TicketDB");
            _tickets = database.GetCollection<Ticket>("Tickets");
        }

        public async Task<List<Ticket>> GetTicketsAsync(int page, int pageSize)
        {
            return await _tickets.Find(ticket => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(ObjectId id)
        {
            return await _tickets.Find(ticket => ticket.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateTicketAsync(Ticket ticket)
        {
            await _tickets.InsertOneAsync(ticket);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            await _tickets.ReplaceOneAsync(t => t.Id == ticket.Id, ticket);
        }

        public async Task DeleteTicketAsync(ObjectId id)
        {
            await _tickets.DeleteOneAsync(t => t.Id == id);
        }

        public async Task<long> GetTicketCountAsync()
        {
            return await _tickets.CountDocumentsAsync(ticket => true);
        }
    }
}