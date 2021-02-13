using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia_LifeCycle.Controllers.Requests
{
    public class UserCredentials
    {
        [Required, StringLength(32, MinimumLength = 4), RegularExpression(@"[a-zA-Z0-9\s._@]{4,32}")]
        public string Login { get; set; }
        [Required, DataType(DataType.EmailAddress), RegularExpression(@"[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password), StringLength(40, MinimumLength = 4)]
        public string Password { get; set; }
        [Required, StringLength(40, MinimumLength = 2), RegularExpression(@"[a-zA-Z0-9\s._\-+*/~^|\\@&$#()%]{1,40}")]
        public string DisplayName { get; set; }
        [RegularExpression(@"^\+[0-9]{2}[0-9]{2,3}[0-9]{9}")]
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "dd/mm/yyyy")]
        public DateTime BirthDate { get; set; }
    }
}