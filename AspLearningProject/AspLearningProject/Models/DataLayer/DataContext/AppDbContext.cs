using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace AspLearningProject.Models.DataLayer.DataContext
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private IConfiguration configuration;
        public AppDbContext()
        {
        }

        public AppDbContext(IConfiguration configuration, DbContextOptions<AppDbContext> options) : base(options)
        {
            this.configuration = configuration;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            const string ADMIN_USER_ID = "22e40406-8a9d-2d82-912c-5d6a640ee696";
            const string ADMIN_ROLE_ID = "b421e928-0613-9ebd-a64c-f10b6a706e73";
            //Seeding a  'Administrator' role to AspNetRoles table
            var t = modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = ADMIN_ROLE_ID, 
                    Name = "Administrator",
                    NormalizedName = "Administrator".ToUpper()
                });


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<IdentityUser>();

            string userEmail = configuration.GetValue<string>("AdminCredential:Email");
            string hashPassword = hasher.HashPassword(null, configuration.GetValue<string>("AdminCredential:Password"));
            //Seeding the User to AspNetUsers table
            var tt = modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = ADMIN_USER_ID, // primary key
                    Email = userEmail,
                    UserName = userEmail,
                    NormalizedUserName = userEmail.ToUpper(),
                    PasswordHash = hashPassword,
                    EmailConfirmed = true,
                    NormalizedEmail = userEmail.ToUpper(),
                    SecurityStamp = string.Empty,

                }
            );

            //Seeding the relation between our user and role to AspNetUserRoles table
            var ttt = modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_USER_ID
                }
            );
        }
      
    }
   
}

