using DataAccess.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess {
    public enum TipKorisnika { Gradjanin = 1, Administrator = 2, NadlezniOrgan = 3 }

    public class AppDbContext : IdentityDbContext<ApplicationUser> {
        public AppDbContext() : base("Data Source=etfsql.database.windows.net;Initial Catalog=KrimiRadDb;User ID=krimirad;Password=1DvaTri!") {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
        }

        public static AppDbContext Create() {
            return new AppDbContext();
            
        }

        public DbSet<Prijava> Prijava { get; set; }
        public DbSet<Medij> Medij { get; set; }
        public DbSet<Album> Album { get; set; }
        public DbSet<TipDjela> TipDjela { get; set; }
    }
}
