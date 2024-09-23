using Microsoft.EntityFrameworkCore;
using Events.EventsDbModels;

namespace Events.Contexts;

public class EventsContext : DbContext
{
    public DbSet<UserDbModel> Users { get; set; }
    public DbSet<CompanyRepresentativeDbModel> Employees { get; set; }
    public DbSet<StudentDbModel> Students { get; set; }
    public DbSet<DeanDbModel> Deans { get; set; }
    public DbSet<CompanyDbModel> Companies { get; set; }
    public DbSet<EventDbModel> Events { get; set; }
    public DbSet<BidDbModel> Bids { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDbModel>()
            .HasDiscriminator<string>("UserType")
            .HasValue<UserDbModel>("User")
            .HasValue<CompanyRepresentativeDbModel>("Employee")
            .HasValue<StudentDbModel>("Student")
            .HasValue<DeanDbModel>("Dean");

        modelBuilder.Entity<CompanyRepresentativeDbModel>()
            .HasOne(e => e.Company)
            .WithMany(c => c.Employees)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventDbModel>()
            .HasOne(e => e.Employee)
            .WithMany()
            .HasForeignKey(e => e.EmployeeId);

        modelBuilder.Entity<EventDbModel>()
            .HasMany(e => e.Students)
            .WithMany(s => s.Events)
            .UsingEntity<Dictionary<string, object>>(
                "StudentEvent",
                je => je.HasOne<StudentDbModel>()
                    .WithMany()
                    .HasForeignKey("StudentId"),
                je => je.HasOne<EventDbModel>()
                    .WithMany()
                    .HasForeignKey("EventId"));

        modelBuilder.Entity<BidDbModel>()
            .HasOne(b => b.Company)
            .WithMany()
            .HasForeignKey(b => b.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public EventsContext(DbContextOptions<EventsContext> options) : base(options) {}
}
