

using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class DataContext: IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        
    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<RequestBook> RequestBooks { get; set; }
    public DbSet<Reserve> Reserves { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<Departament> Departaments {get;set;}
    

}