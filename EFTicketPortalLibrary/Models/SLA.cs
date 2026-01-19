using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFTicketPortalLibrary.Models;

public class SLA
{
    [Key]
    [Column(TypeName = "char(5)")]
    public string SLAid { get; set; }

    [Required]
    [Column(TypeName = "char(5)")]
    [ForeignKey("TicketType")]
    public string TicketTypeId { get; set; }

    public int ResponseTime { get; set; }

    public int ResolutionTime { get; set; }

    public virtual TicketType? TicketType { get; set; }
}

