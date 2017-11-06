using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewLabsTest.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = @"{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
