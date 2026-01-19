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

    public string? ReplyId { get; set;} = null!;



 

    [Column(TypeName = "char(5)")]

    [Required(ErrorMessage = "TicketId is required...")]

    [MaxLength(5, ErrorMessage = "Ticket Id must be exactly 5 characters only.")]

    public string? TicketId { get; set; } = null!;



   

    [Column(TypeName = "char(5)")]

    [Required(ErrorMessage = "EmployeeId is required...")]

    [MaxLength(5, ErrorMessage = "Employee Id must be exactly 5 characters only.")]

    public string? CreatedEmployeeId { get; set;} = null!;



    [Column(TypeName = "char(5)")]

    [Required(ErrorMessage = "Assgned EmployeeId is required...")]

    [MaxLength(5, ErrorMessage = "AssignedEmployee Id must be exactly 5 characters only.")]


    public string? AssignedEmployeeId {get; set;} = null!;



 

    [Column(TypeName = "nvarchar(max)")]

    [Required(ErrorMessage = "Please reply for the text...")]

    public string? ReplyText { get; set;} = null!;


    [Required(ErrorMessage = "Please select the date...")]

    public DateTime RepliedDate { get; set;}

[ForeignKey(nameof(TicketId))]
    [InverseProperty("Replies")]
    public virtual Ticket Ticket { get; set; } = null!;
<<<<<<< HEAD
    [ForeignKey("CreatedEmployeeId")]
    public virtual Employee CreatedEmployee { get; set; } = null!;
    [ForeignKey("AssignedEmployeeId")]
    public virtual Employee AssignedEmployee { get; set; } = null!;
=======

    [ForeignKey(nameof(CreatedEmployeeId))]
    [InverseProperty("CreatedEmpReplies")]
    public virtual Employee CreatedEmployee { get; set; } = null!;

    [ForeignKey(nameof(AssignedEmployeeId))]
    [InverseProperty("AssignedEmpReplies")]
    public virtual Employee? AssignedEmployee { get; set; }
}


>>>>>>> ee80a03c557fd579b88b8184b3b849a7c8184432
 

 

