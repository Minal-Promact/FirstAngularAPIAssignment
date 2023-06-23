using FirstAngularAPIAssignment.Model;
using Microsoft.EntityFrameworkCore;

namespace FirstAngularAPIAssignment.Data
{
    public class EFDataContext : DbContext
    {
        public EFDataContext()
        {

        }

        public EFDataContext(DbContextOptions<EFDataContext> options)
           : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Entity Configuration of "Department" entity 
            // ToTable => configure table name

            modelBuilder.Entity<Skill>().ToTable("skill");
            //Haskey
            modelBuilder.Entity<Skill>().HasKey(a => a.Id);

            //Property configuration "Employee" entity
            modelBuilder.Entity<Employee>().Property(a => a.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id");

            modelBuilder.Entity<Employee>().Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);


            // configures one-to-many relationship

            modelBuilder.Entity<Employee>()
                .HasMany(d => d.Skills)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId);

            // Instead of foreign key attribute or the EF core convetions,
            // We are going to use entity configurations here
            modelBuilder.Entity<Employee>()
                 .HasMany(d => d.Skills)
                 .WithOne(e => e.Employee)
                 .HasForeignKey(e => e.EmployeeId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.UseSerialColumns();

            // Configure other entities...

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("WebApiDatabase");
            }

        }
    }
}
