using ContactsAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class, new()
    {
        protected readonly ContactContext _contactsContext;

        public Repository(ContactContext contactContext)
        {
            _contactsContext = contactContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            try
            {
                await _contactsContext.AddAsync(entity);
                await _contactsContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't add entity: {ex.Message}");
            }
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            try
            {
                if (_contactsContext.Entry(entity) != null && _contactsContext.Entry(entity).State == EntityState.Detached)
                {
                    _contactsContext.Attach(entity);
                }

                _contactsContext.Remove(entity);
                await _contactsContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

       
        public IQueryable<TEntity> GetAll()
        {
            try
            {
                return _contactsContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            try
            {
                _contactsContext.Update(entity);
                await _contactsContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        //Finalizing
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _contactsContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
