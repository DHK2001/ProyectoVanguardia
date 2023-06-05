using System;
using Assistant.Core;
using Assistant.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace UTunes.Infrastructure.Database
{
    public class BaseRepository<TEntity> : IRepository<TEntity>
          where TEntity : class
    {
        private readonly AssistantContext context;

        public BaseRepository(AssistantContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await context.AddAsync(entity);
            return result.Entity;
        }

        public async Task<IReadOnlyList<TEntity>> AllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public int Commit()
        {
            return context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await context.SaveChangesAsync();
        }

        public TEntity Delete(TEntity entity)
        {
            var result = context.Remove(entity);
            return result.Entity;
        }

        public IReadOnlyList<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            return context.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity? GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public TEntity Update(TEntity entity)
        {
            var result = context.Update(entity);
            return result.Entity;
        }

    }
}

