using G10_ProjectDotNet.Models;
using G10_ProjectDotNet.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace G10_ProjectDotNet.Data
{
    public class ApplicationDataInitializer
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public ApplicationDataInitializer(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                var adress1 = new Address() { City = "Brugge", ZipCode = 8000, Street = "Noordzandstraat", Number = 1 };
                _dbContext.Add(adress1);
                var adress2 = new Address() { City = "Zedelgem", ZipCode = 8210, Street = "Leliestraat", Number = 76 };
                _dbContext.Add(adress2);                
                var adress3 = new Address() { City = "Gent", ZipCode = 9000, Street = "Overpoortstraat", Number = 65 };
                _dbContext.Add(adress3);

                var teacher = new Teacher { UserName = "Teacher", Email = "teacher@student.hogent.be", Firstname = "Teacher", Lastname = "Decorte", Address = adress3, Birthday = new DateTime(1950, 11, 29), PhoneNumber = "0498696969" };
                var member = new Member { UserName = "User", Email = "user@student.hogent.be", Firstname = "User", Lastname = "User", Address = adress1, Birthday = new DateTime(1999, 11, 29), PhoneNumber = "0498696969" };
                var member1 = new Member { UserName = "User1", Email = "use1r@student.hogent.be", Firstname = "User", Lastname = "1", Address = adress2, Birthday = new DateTime(1999, 1, 9), PhoneNumber = "0498696969" };
                var member2 = new Member { UserName = "User2", Email = "user2@student.hogent.be", Firstname = "User", Lastname = "2", Address = adress3, Birthday = new DateTime(1999, 5, 6), PhoneNumber = "0498696969"};
                var member3 = new Member { UserName = "User3", Email = "user3@student.hogent.be", Firstname = "User", Lastname = "3", Address = adress1, Birthday = new DateTime(1999, 3, 12), PhoneNumber = "0498696969" };
                var admin = new Admin { UserName = "Robbe", Email = "robbe.decorte@student.hogent.be", Firstname = "Robbe", Lastname = "Decorte", Address = adress1, Birthday = new DateTime(1999, 11, 29), PhoneNumber = "0498696969" };
                var edward = new Admin { UserName = "Edward", Email = "edward.kerckhof@student.hogent.be", Firstname = "Edward", Lastname = "Kerckhof", Address = adress2, Birthday = new DateTime(1999, 4, 5), PhoneNumber = "0498149393" };
                _dbContext.Gebruikers.AddRange(teacher, member, member1, member2, member3, admin, edward);

                await CreateUser(admin, "P@ssword1", "Admin");
                await CreateUser(edward, "P@ssword1", "Admin");
                await CreateUser(teacher, "P@ssword1", "Teacher");
                await CreateUser(member, "P@ssword1", "User");
                await CreateUser(member1, "P@ssword1", "User");
                await CreateUser(member2, "P@ssword1", "User");
                await CreateUser(member3, "P@ssword1", "User");

                var groep = new Group() { Day = Weekday.Maandag, Teacher = teacher };
                var groep1 = new Group() { Day = Weekday.Vrijdag, Teacher = teacher };
                
                var usergroup = new UserGroup() { Member = member, Group = groep };
                _dbContext.Add(usergroup);
                usergroup = new UserGroup() { Member = member, Group = groep1 };
                _dbContext.Add(usergroup);
                usergroup = new UserGroup() { Member = member1, Group = groep1 };
                _dbContext.Add(usergroup);
                usergroup = new UserGroup() { Member = member2, Group = groep1 };
                _dbContext.Add(usergroup);
                usergroup = new UserGroup() { Member = member3, Group = groep };
                _dbContext.Add(usergroup);

                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(ApplicationUser user, string password, string role)
        {
            await _userManager.CreateAsync(new IdentityUser { UserName = user.UserName, Email = user.Email }, password);
            await _userManager.AddClaimAsync(await _userManager.FindByEmailAsync(user.Email), new Claim(ClaimTypes.Role, role));
        }
    }
}
