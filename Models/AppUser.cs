using Microsoft.AspNetCore.Identity;

namespace Chat.Models
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
        public string ConnectionId { get; set; }
    }
}
