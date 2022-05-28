using GetInto.Persistence.Context;
using GetInto.Persistence.Contracts;

namespace GetInto.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly GetIntoContext _context;
        public GeralPersist(GetIntoContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _context.RemoveRange(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

    }
}
