﻿using FiorelloBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FiorelloBackend.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Setting> Settings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasQueryFilter(m=>!m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m=>!m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m=>!m.SoftDeleted);
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);

            //modelBuilder.Entity<Category>()
            //          .HasMany(c => c.Products)
            //          .WithOne(p => p.Category)
            //          .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Product>()
            //            .HasMany(p => p.Images)
            //            .WithOne(pi => pi.Product)
            //            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Setting>().HasData(
               new Setting
               {
                 Id=3,
                 Key = "Phone",
                 Value = "45873458"
               }

            );

        }
    }
}
