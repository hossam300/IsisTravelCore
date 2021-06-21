using System;
using System.Collections.Generic;
using System.Text;
using IsisTravelCore.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IsisTravelCore.Data
{
    public class ApplicationDbContext
      : IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserClaim<string>,
      ApplicationUserRole, IdentityUserLogin<string>,
      IdentityRoleClaim<string>, IdentityUserToken<string>>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
        public DbSet<ApplicationUserRole> ApplicationUserRole { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CatrgoryDayPrice> CatrgoryDayPrices { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryImage> CountryImages { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelImage> HotelImage { get; set; }
        public DbSet<Include> Includes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourActivity> TourActivities { get; set; }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<TourInclude> TourIncludes { get; set; }
        public DbSet<TourService> TourServices { get; set; }
        public DbSet<SildeShow> SildeShows { get; set; }
        public DbSet<TourImage> TourImages { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<RequestOffer> RequestOffers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TourQuote> TourQuotes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<IsisPage> IsisPages { get; set; }
    }
}

