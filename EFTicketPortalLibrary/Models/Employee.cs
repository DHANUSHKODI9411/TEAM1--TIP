using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace EFTicketPortalLibrary.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Column(TypeName = "Char(5)")]
        [Required(ErrorMessage = "Employee ID cannot be Null")]
        [StringLength(5, ErrorMessage = "The Employee Id must be exactly 5 characters")]
        public string EmployeeId { get; set; } = null!;

        [Column(TypeName = "Varchar(50)")]
        [Required(ErrorMessage = "Employee Name cannot be Null")]
        [MaxLength(50, ErrorMessage = "Employee name cannot be more than 50 characters")]
        public string EmployeeName { get; set; } = null!;

        [Column(TypeName = "Varchar(100)")]
        [Required(ErrorMessage = "Email Id cannot be Null")]
        [MaxLength(100, ErrorMessage = "Email Id cannot exceed 100 characters")]
        public string Email { get; set; } = null!;

        [Column(TypeName = "Varchar(15)")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "Password must be between 8 to 15 characters")]
        public string Password { get; set; } = null!;

        [Column(TypeName = "Varchar(20)")]
        [Required(ErrorMessage = "Role cannot be null")]
        [RegularExpression("^(User|Admin)$", ErrorMessage = "Role must be either 'User' or 'Admin'")]
        public string Role { get; set; } = "User";
        
        public virtual ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public virtual ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
    }
}
