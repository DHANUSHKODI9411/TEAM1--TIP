using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTicketPortalLibrary.Models;

[Table("TICKET-TYPE")]
public class TicketType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column(TypeName = "CHAR(5)")]
    [MaxLength(5, ErrorMessage = "Ticket Type Id should not have more than 5 characters")]
    public string? TicketTypeId { get; set; }

    [Required(ErrorMessage = "Ticket Type Name should not be empty")]
    [Column(TypeName = "VARCHAR(100)")]
    [MaxLength(100, ErrorMessage = "Ticket Type Name cannot have more than 100 characters")]
    public string? TicketTypeName { get; set; }

    [Column(TypeName = "VARCHAR(500)")]
    [MaxLength(500, ErrorMessage = "Description cannot have more than 500 characters")]
    public string? Description { get; set; }

    public virtual ICollection<SLA> Slas { get; set; } = new List<SLA>();
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}