using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Core.Entities;
using StockManagement.Api.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace StockManagement.Api.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly StockDbContext _context;

        public UserRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public User GetUser(User userModel)
        {
            return _context.Users.Where(x => x.Email.ToLower() == userModel.Email.ToLower()
                && x.Password == userModel.Password).FirstOrDefault();
        }
        public async Task<ActionResult<User>> GetId(int id)
        {
            return await _context.Users.FindAsync(id);
        }

    }
}
