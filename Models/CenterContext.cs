using Microsoft.EntityFrameworkCore;
 
namespace DojoActivityCenter.Models
{
    public class CenterContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public CenterContext(DbContextOptions<CenterContext> options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<Activity> Activities {get; set;}
        public DbSet<Participant> Participants {get; set;}
    }
}