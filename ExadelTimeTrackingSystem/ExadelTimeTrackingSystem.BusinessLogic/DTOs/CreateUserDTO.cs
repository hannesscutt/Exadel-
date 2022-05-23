namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using ExadelTimeTrackingSystem.Data.Models.Enums;

    public class CreateUserDTO
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public RoleDTO[] Roles { get; set; }
    }
}
