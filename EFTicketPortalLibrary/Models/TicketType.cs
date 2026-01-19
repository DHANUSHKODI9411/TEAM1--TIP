using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTicketPortalLibrary.Models
{
    [Table("TICKET_TYPE")]
    public class TicketType
    {
        [Key]
        [Display(Name = "Type ID")]
        public int TicketTypeId { get; set; }

        [Required(ErrorMessage = "Ticket Type Name is required")]
        [MaxLength(100, ErrorMessage = "Type Name cannot exceed 100 characters")]
        [Display(Name = "Ticket Type Name")]
        public string TicketTypeName { get; set; } = null!;

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        [Display(Name = "Type Description")] 
        public string? Description { get; set; }

        public virtual ICollection<Sla> Slas { get; set; } = new List<Sla>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}