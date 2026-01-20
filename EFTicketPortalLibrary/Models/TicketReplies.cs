using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace EFTicketPortalLibrary.Models;

[Table("TicketReplies")]
public class TicketReplies
{
    [Key]
    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "ReplyId is required...")]
    [MaxLength(5, ErrorMessage = "Reply Id must be exactly 5 characters only.")]
    public string ReplyId { get; set; } = null!;

    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "TicketId is required...")]
    [MaxLength(5, ErrorMessage = "Ticket Id must be exactly 5 characters only.")]
    [ForeignKey("Ticket")] 
    public string TicketId { get; set; } = null!;

    [Column(TypeName = "char(5)")]
    [Required(ErrorMessage = "EmployeeId is required...")]
    [MaxLength(5, ErrorMessage = "Employee Id must be exactly 5 characters only.")]
    [ForeignKey("CreatedEmployee")] 
    public string CreatedEmployeeId { get; set; } = null!;

    [Column(TypeName = "char(5)")]
    [MaxLength(5, ErrorMessage = "AssignedEmployee Id must be exactly 5 characters only.")]
    [ForeignKey("AssignedEmployee")] 
    public string? AssignedEmployeeId { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    [Required(ErrorMessage = "Please reply for the text...")]
    public string ReplyText { get; set; } = null!;

    [Column(TypeName = "DATETIME")]
    [Required(ErrorMessage = "Please select the date...")]
    public DateTime RepliedDate { get; set; }

    public virtual Ticket? Ticket { get; set; }
    public virtual Employee? CreatedEmployee { get; set; }
    public virtual Employee? AssignedEmployee { get; set; }
}
