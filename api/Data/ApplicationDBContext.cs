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

        public DbSet<PLPModel> PLPproductsTable { get; set; }

        public DbSet<PLUserModel> PLUserModels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder); // почему то без этого не добавляется юзер


            // фигачим правила связывания юзеров и Продуктовых листов через таблицу PLUserModel
            modelbuilder.Entity<PLUserModel>(
                            x => x.HasKey(
                                p => new { p.UserId, p.ProductListId }
                            ));



            modelbuilder.Entity<PLUserModel>()
            .HasOne(u => u.productList)
            .WithOne(p => p.User);


            modelbuilder.Entity<PLUserModel>()
            .HasOne(pl => pl.user)
            .WithMany(p => p.PLUserModel)
            .HasForeignKey(p => p.UserId);



            modelbuilder.Entity<PLPModel>(
                            x => x.HasKey(
                                p => new { p.ProductListId, p.ProductId }
                            ));

            modelbuilder.Entity<PLPModel>()
            .HasOne(pl => pl.ProductList)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.ProductListId);

            modelbuilder.Entity<PLPModel>()
            .HasOne(pl => pl.Product)
            .WithMany(p => p.ProductLists)
            .HasForeignKey(p => p.ProductId);


            //фигачим правила связывания юзеров и стоков через таблицу портфолио
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



            modelbuilder.Entity<AppUser>()
                        .HasMany(u => u.PLUserModel)
                        .WithOne(p => p.user)
                        .HasForeignKey(a => a.UserId);

            modelbuilder.Entity<ProductList>()
            .HasOne(a => a.User)
            .WithOne(a => a.productList);

            // Тут задаём возможные роли
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