using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EFTicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTicketPortalLibrary.Models;


public class TicketPortalDbContext : DbContext
{
    public TicketPortalDbContext(DbContextOptions<TicketPortalDbContext> options) : base(options)
    {

}
}