using GetInto.Domain.Identity;
using GetInto.Persistence.Context;
using GetInto.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GetInto.Persistence
{
    public class UserPersist : GeralPersist, IUserPersist
    {
        private readonly GetIntoContext _context;

        public UserPersist(GetIntoContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(
                            user => user.UserName == userName.ToLower()
                         );
        }
    }
}
