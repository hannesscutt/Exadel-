namespace ExadelTimeTrackingSystem.BusinessLogic.DTOs
{
    using System;

    public class UserDTO : CreateUserDTO
    {
        public Guid Id { get; set; }
    }
}
