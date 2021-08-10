using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAPI2.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(3)]
        public string FirstName { get; set; }
        [MinLength(3)]
        public string LastName { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        [StringLength(2)]
        public string Nationality { get; set; }
        public  string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }

    }
}
