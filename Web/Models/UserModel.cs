using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Camp.Models
{
    [Table("User")]
    public class UserModel
    {
        [HiddenInput]
        public virtual int Id { get; set; }

        [StringLength(50)]
        public virtual string Name { get; set; }

        public virtual string PasswordHash { get; set; }
    }
}