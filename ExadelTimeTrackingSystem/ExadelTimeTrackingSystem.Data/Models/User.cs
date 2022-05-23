namespace ExadelTimeTrackingSystem.Data.Models
{
    using System;
    using ExadelTimeTrackingSystem.Data.Models.Abstract;
    using ExadelTimeTrackingSystem.Data.Models.Enums;

    public class User : IDocument
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public Role[] Roles { get; set; }
    }
}