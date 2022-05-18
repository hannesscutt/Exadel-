// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.
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