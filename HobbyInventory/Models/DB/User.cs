using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HobbyInventory.Models.DB
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int? Zipcode { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public bool IsRetired { get; set; }

        public virtual Order Orders { get; set; }
    }
}
