using DBugr.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBugr.Data
{
    public class ApplicationDbContext : IdentityDbContext<BTUser>
    {
        private readonly IConfiguration Configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(
                    DataUtility.GetConnectionString(Configuration),
            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
        }

        public DbSet<DBugr.Models.Company> Company { get; set; }
        public DbSet<DBugr.Models.Invite> Invite { get; set; }
        public DbSet<DBugr.Models.Notification> Notification { get; set; }
        public DbSet<DBugr.Models.Project> Project { get; set; }
        public DbSet<DBugr.Models.ProjectPriority> ProjectPriority { get; set; }
        public DbSet<DBugr.Models.Ticket> Ticket { get; set; }
        public DbSet<DBugr.Models.TicketAttachment> TicketAttachment { get; set; }
        public DbSet<DBugr.Models.TicketComment> TicketComment { get; set; }
        public DbSet<DBugr.Models.TicketHistory> TicketHistory { get; set; }
        public DbSet<DBugr.Models.TicketPriority> TicketPriority { get; set; }
        public DbSet<DBugr.Models.TicketStatus> TicketStatus { get; set; }
        public DbSet<DBugr.Models.TicketType> TicketType { get; set; }
    }
}
