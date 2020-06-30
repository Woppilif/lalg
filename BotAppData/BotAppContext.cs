using BotAppData.Models;
using Microsoft.EntityFrameworkCore;

namespace BotAppData
{
    public class BotAppContext : DbContext
    {
        public DbSet<Age> Ages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupType> GroupTypes { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<PatternMessage> PatternMessages { get; set; }
        public DbSet<LinkSpyer> LinkSpyers { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<LessonLog> LessonLogs { get; set; }
        public DbSet<Referal> Referals { get; set; }
        public BotAppContext(DbContextOptions<BotAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasMany(g => g.Lessons).WithOne(l => l.Group);
            modelBuilder.Entity<Group>().HasOne(x => x.Age);
            modelBuilder.Entity<Group>().HasOne(x => x.GroupType);
            modelBuilder.Entity<Group>().HasOne(x => x.Product);

            modelBuilder.Entity<Lesson>().HasOne(x => x.Group).WithMany(x => x.Lessons);
            modelBuilder.Entity<Lesson>().HasOne(x => x.Pattern);

            modelBuilder.Entity<Pattern>().HasMany(g => g.PatternMessages).WithOne(l => l.Pattern);
            modelBuilder.Entity<PatternMessage>().HasOne(x => x.Pattern).WithMany(x => x.PatternMessages);

            modelBuilder.Entity<User>().HasOne(x => x.Group);
            modelBuilder.Entity<User>().HasOne(x => x.Age);

            modelBuilder.Entity<LessonLog>().HasOne(x => x.Users);
            modelBuilder.Entity<LessonLog>().HasOne(x => x.Lesson);

            modelBuilder.Entity<LinkSpyer>().HasOne(x => x.Users);
            modelBuilder.Entity<LinkSpyer>().HasOne(x => x.Lesson);

            modelBuilder.Entity<Payment>().HasOne(x => x.Users);
            modelBuilder.Entity<Payment>().HasOne(x => x.Subscription);

            modelBuilder.Entity<Product>().HasOne(x => x.ProductType);
            modelBuilder.Entity<Product>().HasMany(x => x.Age);

            modelBuilder.Entity<Subscription>().HasOne(x => x.Users);
            modelBuilder.Entity<Subscription>().HasOne(x => x.Product);

            modelBuilder.Entity<Referal>().HasOne(x => x.Users);
        }
    }
}
