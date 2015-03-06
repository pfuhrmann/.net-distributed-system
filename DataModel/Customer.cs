using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataModel
{
    public class Customer : IdentityUser
    {
        public Customer()
        {
            Orders = new ObservableListSource<Order>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [NotMapped]
        [Display(Name = "Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public bool Gender { get; set; }

        public DateTime Dob { get; set; }

        [Required]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Required]
        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        public string Town { get; set; }

        public virtual ObservableListSource<Order> Orders { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Customer> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
