using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTicketPortalLibrary.Models;
[Table("SLA")]
public class SLA
{
    [Key]
    [Column(TypeName = "char(5)")]
    [MaxLength(5, ErrorMessage = "SLA ID must be exactly 5 characters")]
    public string SLAid { get; set; } = null!;
 
    [Required(ErrorMessage = "Ticket Type ID is required")]
    [Column(TypeName = "char(5)")]
    [MaxLength(5, ErrorMessage = "Ticket Type ID must be exactly 5 characters")]
    [ForeignKey("TicketType")]
    public string TicketTypeId { get; set; } = null!;
 
    [Required(ErrorMessage = "Response time is required")]
    [Range(1, 8760, ErrorMessage = "Response time must be between 1-8760 hours")]
    public int ResponseTime { get; set; }
 
    [Required(ErrorMessage = "Resolution time is required")]
    [Range(1, 8760, ErrorMessage = "Resolution time must be between 1-8760 hours")]
    public int ResolutionTime { get; set; }
 
    [Column(TypeName = "NVARCHAR(500)")]
    [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }
 
    [ForeignKey(nameof(TicketTypeId))]
    [InverseProperty("Slas")]
    public virtual TicketType TicketType { get; set; } = null!;
}

