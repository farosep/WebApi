using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    // тут добавили идентификацию контекста по юзеру 
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) :
            base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<ProductList> ProductLists { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Portfolio> Portfolios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<ProductList>()
            .HasMany(p => p.Products)
            .WithMany();

            base.OnModelCreating(modelbuilder); // почему то без этого не добавляется юзер


            //схема взаимоотношений юзеров и стоков через таблицу портфолио
            modelbuilder.Entity<Portfolio>(
                x => x.HasKey(
                    p => new { p.AppUserId, p.StockId }));

            modelbuilder.Entity<Portfolio>()
            .HasOne(u => u.appUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);

            modelbuilder.Entity<Portfolio>()
            .HasOne(u => u.stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole>{
                new IdentityRole{
                    Name ="Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole{
                    Name ="User",
                    NormalizedName = "USER"
                },
            };
            modelbuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}