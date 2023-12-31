﻿using Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity : class
    {
        protected NeondbContext _context;
        protected DbSet<TEntity> _entities;

        public GenericRepository(NeondbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }
        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            
            var collection =     await _entities.AsNoTracking().ToListAsync();
            return collection;
        }   

        public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _entities.AsNoTracking().Where(predicate).ToList();
        }
        public TEntity? FindById(int id)
        {
            return _entities.Find(id);
        }

        public async Task<TEntity?> CreateAsync(TEntity item)
        {

            await _entities.AddAsync(item);
            _context.SaveChanges();
            return item;
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public async Task<TEntity> Remove(int id)
        {
           var profile  = await _context.FindAsync<TEntity>(id);
            _entities.Remove(profile);
            _context.SaveChanges();
            return profile;
        }


    }
}
