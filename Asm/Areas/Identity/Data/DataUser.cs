using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Asm.Models;
using Microsoft.AspNetCore.Identity;

namespace Asm.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the DataUser class
    public class DataUser : IdentityUser
    {
        [PersonalData]      // dinh kem them vao bang User
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }
        [PersonalData]      // dinh kem them vao bang User
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
