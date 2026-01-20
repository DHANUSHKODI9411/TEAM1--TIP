using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTicketPortalLibrary.Models;

[Table("Status")]
public class Status
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column(TypeName = "CHAR(5)")]
    [MaxLength(5, ErrorMessage = "Status Id should not have more than 5 characters")]
    public string? StatusId { get; set; }

    [Required(ErrorMessage="Staus Name should not be empty")]
    [Column(TypeName = "VARCHAR(30)")]
    [MaxLength(30, ErrorMessage = "Status Name cannot have more than 30 characters")]
    public string StatusName { get; set; }

    [Column(TypeName = "VARCHAR(100)")]
    [MaxLength(100, ErrorMessage = "Description cannot have more than 100 characters")]
    public string? Description { get; set; }

    [Required, Column(TypeName = "BIT")]
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

}
