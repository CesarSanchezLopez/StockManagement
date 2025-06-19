using StockManagement.Core.Enums;
using System.Data;

namespace StockManagement.Api.Core.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; } // enum "Admin" o "User"
    }
}
