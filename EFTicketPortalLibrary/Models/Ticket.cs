using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTicketPortalLibrary.Models;

[Table("TICKET")]
public class Ticket
{
    [Key]
    [Required(ErrorMessage = "Ticket Id is required.")]
    [Column(TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Ticket Id must be exactly 5 characters.")]
    public string TicketId { get; set; } = null!;



    [Required(ErrorMessage = "Title is required.")]
    [Column(TypeName = "VARCHAR(200)")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public string Title { get; set; } = null!;



    [Required(ErrorMessage = "Description is required.")]
    [Column(TypeName = "VARCHAR(4000)")]
    [MaxLength(4000, ErrorMessage = "Description cannot exceed 4000 characters.")]
    public string Description { get; set; } = null!;



    [Required(ErrorMessage = "Employee Id is required.")]
    [Column(TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Employee Id must be exactly 5 characters.")]
    public string CreatedEmployeeId { get; set; } = null!;



    [Required(ErrorMessage = "Ticket Type Id is required.")]
    [Column(TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Ticket Type Id must be exactly 5 characters.")]
    public string TicketTypeId { get; set; } = null!;



    [Required(ErrorMessage = "Status Id is required.")]
    [Column(TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Status Id must be exactly 5 characters.")]
    public string StatusId { get; set; } = null!;



    [Column(TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Assigned Employee Id must be exactly 5 characters.")]
    public string? AssignedEmployeeId { get; set; }


    [ForeignKey(nameof(CreatedEmployeeId))]
    [InverseProperty("CreatedTickets")]
    public virtual Employee Employee { get; set; }

    [ForeignKey(nameof(AssignedEmployeeId))]
    [InverseProperty("AssignedTickets")] 
    public virtual Employee? AssignedEmployee { get; set; }

    [ForeignKey(nameof(TicketTypeId))]
    [InverseProperty("Tickets")] 
    public virtual TicketType TicketType { get; set; } 

    [ForeignKey(nameof(StatusId))]
    [InverseProperty("Tickets")] 
    public virtual Status Status { get; set; }

    
    [InverseProperty("Ticket")] 
    public virtual ICollection<TicketReplies> Replies { get; set; } = new List<TicketReplies>();
}
