using DrugIndication.Application.Interfaces.Repositories;
using DrugIndication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DrugIndication.Infrastructure.Data.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("DrugIndicationDb"));
            var db = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);

            _collection = db.GetCollection<User>("Users");
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
            await _collection.Find(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);

        public async Task CreateAsync(User user, CancellationToken cancellationToken) =>
            await _collection.InsertOneAsync(user, cancellationToken: cancellationToken);
    }
}
