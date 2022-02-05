using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using Core.Entities.Concrete;
using DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            await using TContext context = new TContext();
            return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            await using TContext context = new TContext();
            return context.Set<TEntity>().SingleOrDefault(filter);
        }

        public async Task Add(TEntity entity)
        {
            await using TContext context = new TContext();
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            await using TContext context = new TContext();
            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            await using TContext context = new TContext();
            var deletedEntity = context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }
    }
}