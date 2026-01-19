using System;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

 

namespace EFTicketPortalLibrary.Models;

 
[Table("TicketReplies")]
public class TicketReplies

{

    [Key]

    [Column(TypeName = "char(5)")]

    [Required(ErrorMessage = "ReplyId is required...")]

    [MaxLength(5, ErrorMessage = "Reply Id must be exactly 5 characters only.")]

    public string? ReplyId { get; set;}



 

    [Column(TypeName = "char(5)")]

    [Required(ErrorMessage = "TicketId is required...")]

    [MaxLength(5, ErrorMessage = "Ticket Id must be exactly 5 characters only.")]

    [ForeignKey("Ticket")]

    public string? TicketId { get; set; }



   

    [Column(TypeName = "char(5)")]

    [Required(ErrorMessage = "EmployeeId is required...")]

    [MaxLength(5, ErrorMessage = "Employee Id must be exactly 5 characters only.")]

    [ForeignKey("Employee")]

    public string? CreatedEmployeeId { get; set;}



 

    [Column(TypeName = "char(5)")]

    [Required(ErrorMessage = "Assgned EmployeeId is required...")]

    [MaxLength(5, ErrorMessage = "AssignedEmployee Id must be exactly 5 characters only.")]

    [ForeignKey("Employee")]

    public string? AssignedEmployeeId {get; set;}



 

    [Column(TypeName = "nvarchar(max)")]

    [Required(ErrorMessage = "Please reply for the text...")]

    public string? ReplyText { get; set;}


 

    [Required(ErrorMessage = "Please select the date...")]

    public DateTime RepliedDate { get; set;}



 

    public virtual Employee? Employee { get; set;}


 
public virtual ICollection<TicketReplies>? Replies { get; set; }
 

 

 

}