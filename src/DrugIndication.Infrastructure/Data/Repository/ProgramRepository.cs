using DrugIndication.Application.Interfaces.Repositories;
using DrugIndication.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DrugIndication.Infrastructure.Data.Repository
{
    internal class ProgramRepository : IProgramRepository
    {
        private readonly IMongoCollection<Program> _collection;

        public ProgramRepository(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("DrugIndicationDb"));
            var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);

            _collection = database.GetCollection<Program>("Programs");
        }

        public async Task<IEnumerable<Program>> GetAllAsync(CancellationToken cancellationToken) =>
            await _collection.Find(_ => true).ToListAsync(cancellationToken);

        public async Task<Program?> GetByIdAsync(string id, CancellationToken cancellationToken) =>
            await _collection.Find(p => p.Id == id).FirstOrDefaultAsync(cancellationToken);

        public async Task CreateAsync(Program program, CancellationToken cancellationToken) =>
            await _collection.InsertOneAsync(program, cancellationToken: cancellationToken);

        public async Task UpdateAsync(Program program, CancellationToken cancellationToken) =>
            await _collection.ReplaceOneAsync(p => p.Id == program.Id, program, cancellationToken: cancellationToken);

        public async Task DeleteAsync(string id, CancellationToken cancellationToken) =>
            await _collection.DeleteOneAsync(p => p.Id == id, cancellationToken);
    }
}
