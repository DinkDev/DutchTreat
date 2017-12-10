namespace DutchTreat.Data.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class StoreUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
