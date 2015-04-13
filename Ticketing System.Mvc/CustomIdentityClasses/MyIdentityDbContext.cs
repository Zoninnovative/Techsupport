using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Ticketing_System.Mvc.CustomIdentityClasses
{
    public class MyIdentityDbContext : IdentityDbContext<MyIdentityUser>
    {
        public MyIdentityDbContext()
            : base("connectionstring")
        {

        }
    }


    public class MyIdentityUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
    }

    public class MyIdentityRole : IdentityRole
    {
        public MyIdentityRole()
        {

        }

        public MyIdentityRole(string roleName, string description)
            : base(roleName)
        {
            this.Description = description;
        }

        public string Description { get; set; }
    }



    public class CustomUserValidator<TUser> : IIdentityValidator<TUser>
    where TUser : class, Microsoft.AspNet.Identity.IUser
    {
        private static readonly Regex EmailRegex = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private readonly UserManager<TUser> _manager;

        public CustomUserValidator()
        {
        }

        public CustomUserValidator(UserManager<TUser> manager)
        {
            _manager = manager;
        }

        public async Task<IdentityResult> ValidateAsync(TUser item)
        {
            var errors = new List<string>();
            if (!EmailRegex.IsMatch(item.UserName))
                errors.Add("Enter a valid email address.");

            if (_manager != null)
            {
                var otherAccount = await _manager.FindByNameAsync(item.UserName);
                if (otherAccount != null && otherAccount.Id != item.Id)
                    errors.Add("Select a different email address. An account has already been created with this email address.");
            }

            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;
        }
    }
}