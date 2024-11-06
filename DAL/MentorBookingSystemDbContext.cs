using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL;

public partial class MentorBookingSystemDbContext : DbContext
{
    public MentorBookingSystemDbContext()
    {
    }

    public MentorBookingSystemDbContext(DbContextOptions<MentorBookingSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Slot> Slots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(GetConnectionString());
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn!;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Slot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Slot_pk");

            entity.ToTable("Slot");

            entity.HasOne(d => d.Mentor).WithMany(p => p.SlotMentors)
                .HasForeignKey(d => d.MentorId)
                .HasConstraintName("Slot_User_Id_fk_2");

            entity.HasOne(d => d.Student).WithMany(p => p.SlotStudents)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("Slot_User_Id_fk");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pk");

            entity.ToTable("User");

            entity.Property(e => e.Password).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
