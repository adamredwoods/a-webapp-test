using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<UserData> UserData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>().HasData(
                new UserData
                {
                    Id = 1,
                    Name = "User1",
                    Password = "123456",
                    GroupRole = UserGroup.User()
                },
                new UserData
                {
                    Id = 2,
                    Name = "User2",
                    Password = "123456",
                    GroupRole = UserGroup.User()
                },
                new UserData
                {
                    Id = 3,
                    Name = "HelpdeskUser",
                    Password = "123456",
                    GroupRole = UserGroup.HelpdeskUser()
                },
                new UserData
                {
                    Id = 4,
                    Name = "HelpdeskTeamMember",
                    Password = "123456",
                    GroupRole = UserGroup.HelpdeskTeamMember()
                }
            );
            modelBuilder.Entity<UserData>().ToTable("UserData");

            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    Id = 1,
                    OwnerId = 1,
                    Status = "open",
                    Words = "123456"
                },
                new Ticket
                {
                    Id = 2,
                    OwnerId = 1,
                    Status = "closed",
                    Words = "asdf"
                },
                new Ticket
                {
                    Id = 3,
                    OwnerId = 2,
                    Status = "open",
                    Words = "sdfgsdfg"
                }
            );
        }
    }
}
