using System;
using System.Collections.Generic;
using System.Text;

namespace Goder.BL.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? Birthday { get; set; }
        public string Email { get; set; }
        public string AvatarURL { get; set; }
    }
}
