using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRC_Project.Data.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Key]
        [StringLength(50)]
        public string OrderId { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        public bool DelFlg { get; set; }
        [StringLength(50)]
        public string InsBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsDatetime { get; set; }
        [StringLength(50)]
        public string UpdBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdDatetime { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(10)]
        public string Phone { get; set; }
        public double? Total { get; set; }

        [ForeignKey(nameof(Username))]
        [InverseProperty(nameof(Users.Orders))]
        public virtual Users UsernameNavigation { get; set; }
        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
