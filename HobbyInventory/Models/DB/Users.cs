using System;
using System.Collections.Generic;

namespace HobbyInventory.Models.DB
{
    public partial class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int? Zipcode { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }

        public virtual Orders Orders { get; set; }
    }
}
