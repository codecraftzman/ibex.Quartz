using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Quartz.Shared.Integration.Contracts;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quartz.Shared.Integration.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        //private readonly DbContext _context;
        public BaseRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        //public BaseRepository(DbContext context)
        //{
        //    _context = context;
        //}

        public async Task<IEnumerable<T>> ListAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
            //return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
            //return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            //await _dbContext.Set<T>().AddAsync(entity);
            //await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, T entity)
        {
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", id), entity);
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id));
            //_dbContext.Set<T>().Remove(entity);
            //await _dbContext.SaveChangesAsync();
        }



    }
}
