using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia_LifeCycle.Controllers
{
    public class UserCredentials
    {
        [Required, StringLength(32)]
        public string Login { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), StringLength(40, MinimumLength = 4)]
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime BirthDate { get; set; }
    }
}