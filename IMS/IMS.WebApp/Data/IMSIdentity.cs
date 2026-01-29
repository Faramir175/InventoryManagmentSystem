using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IMS.WebApp.Data
{
    public class IMSIdentity(DbContextOptions<IMSIdentity> options) : IdentityDbContext<IdentityUser>(options)
    {
    }
}
