using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace BookShopBusiness
{
    public class BookDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Shipping> Shippings { get; set; }
        public DbSet<User> Users { get; set; }
        public BookDbContext(DbContextOptions<BookDbContext> options)
       : base(options)
        {
        }

        public BookDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("BookStore"));
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.UserSubmit)
                .WithMany(u => u.SubmittedShippings)
                .HasForeignKey(s => s.UserSubmitId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Shipping>()
                .HasOne(s => s.UserApprove)
                .WithMany(u => u.ApprovedShippings)
                .HasForeignKey(s => s.UserApproveId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category) // Thay thế với thuộc tính Category trong Book
                .WithMany(c => c.Books) // Thay thế với thuộc tính Books trong Category
                .HasForeignKey(b => b.CategoryId); // Khóa ngoại trong Book
        }
}
}
