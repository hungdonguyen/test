using Microsoft.VisualStudio.Services.UserAccountMapping;

namespace TestBackEnd.Models
{
    public class Role
    {
        private int RoleId { get; set; }
        private string RoleName { get; set; } // e.g., “Admin”, “User”, "Librarian" ,....
        private string Description { get; set; }
        public required ICollection<UserRole> UserRoles { get; set; }

    }
}
