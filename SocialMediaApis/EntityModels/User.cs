using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApis.Models
{
    [Table("tbl_User")]
    public class User
    {
        [Key]
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get;set; }
        public string Password { get;set;}
        public string ProfileDescription { get;set; }
        public string ProfileImage { get; set; }
        public string BackgroundImage { get;set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get;set ; }
        public DateTime UpdateDate { get;set ; }
        public int DeletedBy { get;set; }
    }
}
