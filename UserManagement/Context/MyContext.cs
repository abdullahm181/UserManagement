using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> dbContext) : base(dbContext)
        {

        }
        //Buat pakai relasi otomatis
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }*/
        public DbSet<IErrorApplication> IErrorApplication { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuWithRole> MenuWithRole { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserActivity> UserActivity { get; set; }
        public DbSet<UserFoto> UserFoto { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
    }
}
