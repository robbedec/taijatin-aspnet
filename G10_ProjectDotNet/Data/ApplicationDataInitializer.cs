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
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDataInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await CreateUser(new Admin { UserName = "Robbe", Email = "robbe.decorte@student.hogent.be" }, "P@ssword1", "Admin");
                await CreateUser(new Teacher { UserName = "Teacher", Email = "teacher@student.hogent.be" }, "P@ssword1", "Teacher");
                await CreateUser(new Member { UserName = "User", Email = "user@student.hogent.be" }, "P@ssword1", "User");
                await CreateUser(new Member { UserName = "User1", Email = "use1r@student.hogent.be" }, "P@ssword1", "User");
                await CreateUser(new Member { UserName = "User2", Email = "user2@student.hogent.be" }, "P@ssword1", "User");
                await CreateUser(new Member { UserName = "User3", Email = "user3@student.hogent.be" }, "P@ssword1", "User");

                var groep = new Group() { Day = Weekday.Maandag, Teacher = (Teacher)await _userManager.FindByNameAsync("Teacher") };
                var groep1 = new Group() { Day = Weekday.Vrijdag, Teacher = (Teacher)await _userManager.FindByNameAsync("Teacher") };
                
                var usergroup = new UserGroup() { Member = (Member)await _userManager.FindByNameAsync("User"), Group = groep };
                _dbContext.Add(usergroup);
                usergroup = new UserGroup() { Member = (Member)await _userManager.FindByNameAsync("User"), Group = groep1 };
                _dbContext.Add(usergroup);
                usergroup = new UserGroup() { Member = (Member)await _userManager.FindByNameAsync("User1"), Group = groep1 };
                _dbContext.Add(usergroup);
                usergroup = new UserGroup() { Member = (Member)await _userManager.FindByNameAsync("User2"), Group = groep1 };
                _dbContext.Add(usergroup);
                usergroup = new UserGroup() { Member = (Member)await _userManager.FindByNameAsync("User3"), Group = groep };
                _dbContext.Add(usergroup);


                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(ApplicationUser user, string password, string role)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
        }
    }
}
