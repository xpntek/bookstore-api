using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace Persistence.Seed;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly DataContext _context;

    public DbInitializer(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        DataContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }

    public void Initilize()
    {
        //Apply pending migrations
        try
        {
            if (_context.Database.GetPendingMigrations().Count() > 0) _context.Database.Migrate();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        if (!_context.Categories.Any())
        {
            _context.Categories.Add(new Category { Description = "Romance" });
            _context.Categories.Add(new Category { Description = "Drama" });
            _context.Categories.Add(new Category { Description = "Novela" });
            _context.Categories.Add(new Category { Description = "Conto" });
            _context.Categories.Add(new Category { Description = "Crônica" });
            _context.Categories.Add(new Category { Description = "Ensaio" });
            _context.Categories.Add(new Category { Description = "Poesia" });
            _context.Categories.Add(new Category { Description = "Carta" });
            _context.Categories.Add(new Category { Description = "Aventura" });
            _context.Categories.Add(new Category { Description = "Biografia" });
            _context.Categories.Add(new Category { Description = "Terror" });
            _context.Categories.Add(new Category { Description = "Acadêmico" });
            _context.Categories.Add(new Category { Description = "Comedia" });
            _context.SaveChanges();
        }

        //Create roles
        if (!_roleManager.RoleExistsAsync(SD.ADMIN_ROLE).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.ADMIN_ROLE)).GetAwaiter().GetResult();

            // Create Admin User
            _userManager.CreateAsync(new ApplicationUser
            {
                Email = "admin@bookstore.com",
                UserName = "sysadmin",
                Fullname = "System Admin",
                PhoneNumber = "879147914"
            }, "Pa$$w0rd").GetAwaiter().GetResult();

            var user = _context.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Email == "20180376@isctem.ac.mz").GetAwaiter().GetResult();

            _userManager.AddToRoleAsync(user, SD.ADMIN_ROLE).GetAwaiter().GetResult();
        }
        
    }
}