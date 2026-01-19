using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFTicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTicketPortalLibrary.Models;


public class TicketPortalDbContext : DbContext
{
    public TicketPortalDbContext()
    {
        
    }
    public TicketPortalDbContext(DbContextOptions<TicketPortalDbContext> options) : base(options)
    {
        
    }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<TicketReplies> TicketReplies { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<TicketType> TicketTypes { get; set; }
    public virtual DbSet<SLA> SLAs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("data source=DESKTOP-BO7RKUV\\SQLEXPRESS; database =TicketPortalDB; integrated security=true; Trust Server Certificate=true");
    }
}