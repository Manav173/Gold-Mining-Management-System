using Gold_Mining_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Sites> Sites { get; set; }
        public DbSet<SafetyIncident> SafetyIncidents { get; set; }
        public DbSet<Reports> Reports { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<MinePlans> MinePlans { get; set; }
        public DbSet<GeologicalData> GeologicalData { get; set; }
        public DbSet<Equipments> Equipments { get; set; }
        public DbSet<EnvironmentalData> EnvironmentalData { get; set; }
        public DbSet<CostManagement> CostManagement { get; set; }

        // Configure the relationships using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the relationship between Sites and Manager
            modelBuilder.Entity<Sites>()
                .HasOne(s => s.Manager)
                .WithMany()  
                .HasForeignKey(s => s.ManagerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuring the relationship between SafetyIncident and Reporter
            modelBuilder.Entity<SafetyIncident>()
                .HasOne(si => si.Reporter)
                .WithMany()
                .HasForeignKey(si => si.ReportedBy)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between Report and Creator
            modelBuilder.Entity<Reports>()
                .HasOne(r => r.Creator)
                .WithMany()
                .HasForeignKey(r => r.CreatedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuring the relationship between Production and Supervisor
            modelBuilder.Entity<Production>()
                .HasOne(p => p.Supervisor)
                .WithMany()
                .HasForeignKey(p => p.ShiftSupervisor)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuring the relationship between Production and Sites
            modelBuilder.Entity<Production>()
                .HasOne(p => p.Site)
                .WithMany()
                .HasForeignKey(p => p.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between Notification and Sender
            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.Sender)
                .WithMany()
                .HasForeignKey(n => n.SendFrom)
                .OnDelete(DeleteBehavior.NoAction);

            // Configuring the relationship between Notification and Receiver
            modelBuilder.Entity<Notifications>()
                .HasOne(n => n.Receiver)
                .WithMany()
                .HasForeignKey(n => n.SendTo)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between MinePlans and Engineer
            modelBuilder.Entity<MinePlans>()
                .HasOne(mp => mp.Engineer)
                .WithMany()
                .HasForeignKey(mp => mp.AssignedEngineer)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between MinePlans and Site
            modelBuilder.Entity<MinePlans>()
                .HasOne(mp => mp.Site)
                .WithMany()
                .HasForeignKey(mp => mp.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between GeologicalData and Geologist
            modelBuilder.Entity<GeologicalData>()
                .HasOne(gd => gd.Geologist)
                .WithMany()
                .HasForeignKey(gd => gd.GeologistId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuring the relationship between GeologicalData and Site
            modelBuilder.Entity<GeologicalData>()
                .HasOne(gd => gd.Site)
                .WithMany()
                .HasForeignKey(gd => gd.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between GeologicalData and Reports
            modelBuilder.Entity<GeologicalData>()
                .HasOne(gd => gd.SurveyReport) 
                .WithMany()
                .HasForeignKey(gd => gd.ReportId)  
                .OnDelete(DeleteBehavior.SetNull);

            // Configuring the relationship between Equipments and Sites 
            modelBuilder.Entity<Equipments>()
                .HasOne(e => e.Site)
                .WithMany()
                .HasForeignKey(e => e.AssignedSite)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuring the relationship between EnvironmentalData and Sites 
            modelBuilder.Entity<EnvironmentalData>()
                .HasOne(ed => ed.Site)
                .WithMany()
                .HasForeignKey(ed => ed.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between EnvironmentalData and Officer
            modelBuilder.Entity<EnvironmentalData>()
                .HasOne(ed => ed.Officer)
                .WithMany()
                .HasForeignKey(ed => ed.ConductedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // Configuring the relationship between CostManagement and Sites 
            modelBuilder.Entity<CostManagement>()
                .HasOne(cm => cm.Site)
                .WithMany()
                .HasForeignKey(cm => cm.SiteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuring the relationship between CostManagement and Manager
            modelBuilder.Entity<CostManagement>()
                .HasOne(cm => cm.Manager)
                .WithMany()
                .HasForeignKey(cm => cm.ResponsiblePerson)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}