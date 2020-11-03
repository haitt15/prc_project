using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRC_Project.Data.Models
{
    public partial class UserDevice
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Username { get; set; }
        [StringLength(255)]
        public string DeviceId { get; set; }

        [ForeignKey(nameof(Username))]
        [InverseProperty(nameof(Users.UserDevice))]
        public virtual Users UsernameNavigation { get; set; }
    }
}
