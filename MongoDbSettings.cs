using System;

namespace MyApi.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = "mongodb://localhost:27017/TicketDB";
        public string Database { get; set; } = "TicketDB";
    }
}