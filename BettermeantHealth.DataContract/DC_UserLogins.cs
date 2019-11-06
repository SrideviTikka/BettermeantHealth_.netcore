using System;
using System.Collections.Generic;
using System.Text;

namespace BettermeantHealth.DataContract
{
    public class DC_UserLogins:DataOperationResponse
    {
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string NPI_Id { get; set; }
    }
}
