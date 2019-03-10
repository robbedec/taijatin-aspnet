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
                var adress2 = new Address() { City = "Zedelgem", ZipCode = 8210, Street = "Leliestraat", Number = 76 };
                var adress3 = new Address() { City = "Gent", ZipCode = 9000, Street = "Overpoortstraat", Number = 65 };
                var adress4 = new Address() { City = "Gent", ZipCode = 9000, Street = "De Pintelaan", Number = 260 };
                _dbContext.AddRange(adress1, adress2, adress3, adress4);

                var teacher = new Teacher { UserName = "Teacher", Email = "teacher@student.hogent.be", Firstname = "Teacher", Lastname = "Decorte", Address = adress3, Birthday = new DateTime(1950, 11, 29), PhoneNumber = "0498696969" };
                var member = new Member { UserName = "User", Email = "user@student.hogent.be", Firstname = "User", Lastname = "User", Address = adress1, Birthday = new DateTime(1999, 11, 29), PhoneNumber = "0498696969", Grade = Grade.Vijfde_Kyu };
                var member1 = new Member { UserName = "User1", Email = "use1r@student.hogent.be", Firstname = "User", Lastname = "1", Address = adress2, Birthday = new DateTime(1999, 1, 9), PhoneNumber = "0498696969", Grade = Grade.Tweede_Kyu };
                var member2 = new Member { UserName = "User2", Email = "user2@student.hogent.be", Firstname = "User", Lastname = "2", Address = adress3, Birthday = new DateTime(1999, 5, 6), PhoneNumber = "0498696969", Grade = Grade.Zesde_Kyu};
                var member3 = new Member { UserName = "User3", Email = "user3@student.hogent.be", Firstname = "User", Lastname = "3", Address = adress1, Birthday = new DateTime(1999, 3, 12), PhoneNumber = "0498696969", Grade = Grade.Vijfde_Kyu };
                var defaultMember = new Member { UserName = "Default", Email = "default@student.hogent.be", Address = new Address() };
                var admin = new Admin { UserName = "Robbe", Email = "robbe.decorte@student.hogent.be", Firstname = "Robbe", Lastname = "Decorte", Address = adress1, Birthday = new DateTime(1999, 11, 29), PhoneNumber = "0498696969" };
                var edward = new Admin { UserName = "Edward", Email = "edward.kerckhof@student.hogent.be", Firstname = "Edward", Lastname = "Kerckhof", Address = adress2, Birthday = new DateTime(1999, 4, 5), PhoneNumber = "0498149393" };
                var jonas = new Admin { UserName = "Jonas", Email = "jonas.baert@student.hogent.be", Firstname = "Jonas", Lastname = "Baert", Address = adress4, Birthday = new DateTime(1996, 11, 26), PhoneNumber = "0492982876" };
                _dbContext.Gebruikers.AddRange(teacher, member, member1, member2, member3, defaultMember, admin, edward, jonas);

                await CreateUser(admin, "P@ssword1", "Admin");
                await CreateUser(edward, "P@ssword1", "Admin");
                await CreateUser(jonas, "P@ssword1", "Admin");
                await CreateUser(teacher, "P@ssword1", "Teacher");
                await CreateUser(member, "P@ssword1", "User");
                await CreateUser(member1, "P@ssword1", "User");
                await CreateUser(member2, "P@ssword1", "User");
                await CreateUser(member3, "P@ssword1", "User");
                await CreateUser(defaultMember, "P@ssword1", "User");

                var formDinsdag = new FormulaDay
                {
                    Day = Weekday.Dinsdag,
                    StartTime = new TimeSpan(18, 0, 0),
                    EndTime = new TimeSpan(20, 0, 0)
                };
                var formWoensdag = new FormulaDay
                {
                    Day = Weekday.Woensdag,
                    StartTime = new TimeSpan(14, 0, 0),
                    EndTime = new TimeSpan(15, 30, 0)
                };
                var formDonderdag = new FormulaDay
                {
                    Day = Weekday.Donderdag,
                    StartTime = new TimeSpan(18, 0, 0),
                    EndTime = new TimeSpan(20, 0, 0)
                };
                var formZaterdag = new FormulaDay
                {
                    Day = Weekday.Zaterdag,
                    StartTime = new TimeSpan(10, 0, 0),
                    EndTime = new TimeSpan(11, 30, 0)
                };
                var formZondag = new FormulaDay
                {
                    Day = Weekday.Zondag,
                    StartTime = new TimeSpan(11, 0, 0),
                    EndTime = new TimeSpan(12, 30, 0)
                };

                var formula = new Formula()
                {
                    FormulaName = "DI_DO",
                    Teacher = teacher,
                    Members = { member, member1, member2, member3 }
                };
                var formula1 = new Formula()
                {
                    FormulaName = "DI_ZA",
                    Teacher = teacher,
                    Members = {  }
                };
                var formula2 = new Formula()
                {
                    FormulaName = "WO_ZA",
                    Teacher = teacher,
                    Members = { member, member1, member3 }
                };
                var formula3 = new Formula()
                {
                    FormulaName = "WO",
                    Teacher = teacher,
                    Members = { defaultMember }
                };
                var formula4 = new Formula()
                {
                    FormulaName = "ZA",
                    Teacher = teacher,
                    Members = { member1 }
                };
                _dbContext.AddRange(formula, formula1, formula2, formula3, formula4);
                _dbContext.AddRange(formDinsdag, formDonderdag, formWoensdag, formZaterdag, formZondag);

                var dag = new FormulaFormulaDay() { Formula = formula2, FormulaDay = formWoensdag };
                _dbContext.Add(dag);
                dag = new FormulaFormulaDay() { Formula = formula2, FormulaDay = formZaterdag };
                _dbContext.Add(dag);
                dag = new FormulaFormulaDay() { Formula = formula3, FormulaDay = formWoensdag };
                _dbContext.Add(dag);

                var attendance = new Attendance { Member = member };
                var attendance1 = new Attendance { Member = member2 };


                // Sessions seeden    
                
                _dbContext.Sessions.Add(new Session { Formulas = new List<Formula> { formula2, formula3 }, Attendances = new List<Attendance> { attendance, attendance1 } });

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
