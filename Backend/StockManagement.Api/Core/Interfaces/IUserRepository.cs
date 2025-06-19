using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Core.Entities;

namespace StockManagement.Api.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();

        User GetUser(User user);
        Task<ActionResult<User>> GetId(int id);
    }
}
