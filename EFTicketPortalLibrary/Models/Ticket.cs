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
    [ForeignKey ("Employee")]
    public string CreatedEmployeeId { get; set; } = null!;



    [Required(ErrorMessage = "Ticket Type Id is required.")]
    [Column(TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Ticket Type Id must be exactly 5 characters.")]
    [ForeignKey("TicketType")]
    public string TicketTypeId { get; set; } = null!;



    [Required(ErrorMessage = "Status Id is required.")]
    [Column(TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Status Id must be exactly 5 characters.")]
    [ForeignKey("Status")]
    public string StatusId { get; set; } = null!;



    [Column(TypeName = "char(5)")]
    [StringLength(5, ErrorMessage = "Assigned Employee Id must be exactly 5 characters.")]
    [ForeignKey ("Employee")]
    public string? AssignedEmployeeId { get; set; }


    public virtual Employee Employee { get; set; } = null!;

    public virtual Employee? AssignedEmployeeID { get; set; }

    public virtual TicketType TicketType { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TicketReplies>? Replies { get; set; }
}
