namespace Data
{
    using System.Data.Entity;

    using Data.Migrations;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class ESportsEventsContext : IdentityDbContext<ApplicationUser>
    {
        public ESportsEventsContext()
            : base("ESportsEventsContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ESportsEventsContext, Configuration>());
        }

        public static ESportsEventsContext Create()
        {
            return new ESportsEventsContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
