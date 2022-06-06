using System;
using System.Linq;
using Core.Entities.Concrete;
using Entities.Concrete;
using Core.Entities.Concrete.User;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Concrete.Employee;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class NorthwindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"Server=localhost;database=context;user=root;password=55431921");
        }

        public DbSet<Product> Product { get; set; }

        public DbSet<Category> Category { get; set; }

        //USER

        public DbSet<User> Users { get; set; }

        public DbSet<OperationClaim> OperationClaims { get; set; }

        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        //EMPLOYEE

        public DbSet<Employee> Employee { get; set; }

        public DbSet<EmployeeOperationClaim> EmployeeOperationClaims { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDeleted);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var item in ChangeTracker.Entries<Product>().Where(e=>e.State==EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.CurrentValues["IsDeleted"] = true;
            }

            return base.SaveChanges();
        }
    }
}
