using Microsoft.EntityFrameworkCore;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Models;

public class TicketPortalDbContext : DbContext
{
    public TicketPortalDbContext() { }
    public TicketPortalDbContext(DbContextOptions<TicketPortalDbContext> options) : base(options) { }

    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<Ticket> Tickets { get; set; }
    public virtual DbSet<TicketReplies> TicketReplies { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<TicketType> TicketTypes { get; set; }
    public virtual DbSet<SLA> SLAs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("data source=localhost\\SQLEXPRESS;database=TicketPortalDB;integrated security=true;TrustServerCertificate=true");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().Property(e => e.EmployeeId).HasMaxLength(5).IsFixedLength().IsRequired();
        modelBuilder.Entity<Ticket>().Property(t => t.TicketId).HasMaxLength(5).IsFixedLength().IsRequired();
        modelBuilder.Entity<Ticket>().Property(t => t.StatusId).HasMaxLength(5).IsFixedLength().IsRequired();  // StatusId CHAR(4)
        modelBuilder.Entity<TicketReplies>().Property(r => r.ReplyId).HasMaxLength(5).IsFixedLength().IsRequired();
        modelBuilder.Entity<SLA>().Property(s => s.SLAid).HasMaxLength(5).IsFixedLength().IsRequired();
        modelBuilder.Entity<Status>().Property(s => s.StatusId).HasMaxLength(5).IsFixedLength().IsRequired();
        modelBuilder.Entity<TicketType>().Property(t => t.TicketTypeId).HasMaxLength(5).IsFixedLength().IsRequired();

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.CreatedEmployee)
            .WithMany(e => e.CreatedTickets)
            .HasForeignKey(t => t.CreatedEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
 
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.AssignedEmployee)
            .WithMany(e => e.AssignedTickets)
            .HasForeignKey(t => t.AssignedEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.TicketType)
            .WithMany(tt => tt.Tickets)
            .HasForeignKey(t => t.TicketTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Status)
            .WithMany(s => s.Tickets)
            .HasForeignKey(t => t.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SLA>()
            .HasOne(s => s.TicketType)
            .WithMany(tt => tt.Slas)
            .HasForeignKey(s => s.TicketTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TicketReplies>()
            .HasOne(r => r.Ticket)
            .WithMany(t => t.Replies)
            .HasForeignKey(r => r.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<TicketReplies>()
            .HasOne(r => r.CreatedEmployee)
            .WithMany()
            .HasForeignKey(r => r.CreatedEmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TicketReplies>()
            .HasOne(r => r.AssignedEmployee)
            .WithMany()
            .HasForeignKey(r => r.AssignedEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Employee>().Property(e => e.Role).HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}
