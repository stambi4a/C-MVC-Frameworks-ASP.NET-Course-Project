namespace Data
{
    using System.Data.Entity;

    using Migrations;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Models.Images;
    using Models.Videos;

    public class ESportsEventsContext : IdentityDbContext<RegisteredUser>
    {
        public ESportsEventsContext()
            : base("ESportsEventsContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ESportsEventsContext, Configuration>());
        }

        //public DbSet<RegisteredUser> RegisteredUsers { get; set; }

        public DbSet<Player> Players { get; set; }

        //public DbSet<Sponsor> Sponsors { get; set; }

        public DbSet<MatchImage> MatchImages { get; set; }

        public DbSet<MatchVideo> MatchVideos { get; set; }

        public DbSet<MapImage> MapImages { get; set; }

        public DbSet<PlayerImage> PlayerImages { get; set; }

        public DbSet<Logo> Logos { get; set; }

        public DbSet<ArticleImage> ArticleImages { get; set; }

        public DbSet<ArticleVideo> ArticleVideos { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Map> Maps { get; set; }

        public DbSet<Article> Articles { get; set; }

        public static ESportsEventsContext Create()
        {
            return new ESportsEventsContext();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasOptional(e => e.Logo).WithRequired(l => l.Event);
            modelBuilder.Entity<Player>().HasOptional(e => e.PlayerImage).WithRequired(l => l.Player);
            modelBuilder.Entity<Match>().HasRequired(p => p.FirstPlayer).WithMany(e => e.MatchesAsFirstPlayer).WillCascadeOnDelete(false);
            modelBuilder.Entity<Match>().HasRequired(p => p.FirstPlayer).WithMany(e => e.MatchesAsSecondPlayer).WillCascadeOnDelete(false);
            modelBuilder.Entity<Map>().HasOptional(p => p.MapImage).WithRequired(e => e.Map);

            modelBuilder.Entity<RegisteredUser>().HasMany(ru => ru.Events).WithMany(e => e.Users).Map(
                e =>
                    {
                        e.MapLeftKey("UserId");
                        e.MapRightKey("EventId");
                        e.ToTable("UsersEvents");
                    });
            modelBuilder.Entity<Player>().HasMany(p => p.Events).WithMany(e => e.Players).Map(
                e =>
                    {
                        e.MapLeftKey("PlayerId");
                        e.MapRightKey("EventId");
                        e.ToTable("PlayersEvents");
                    });
            modelBuilder.Entity<Match>().HasRequired(e => e.Event).WithMany(l => l.Matches);
            modelBuilder.Entity<Match>().HasMany(p => p.Games).WithRequired(e => e.Match);
            modelBuilder.Entity<Game>().HasRequired(p => p.Map).WithMany(e => e.Games);
            modelBuilder.Entity<Event>().HasMany(p => p.MapPool).WithMany(e => e.Events).Map(
                e =>
                    {
                        e.MapLeftKey("EventId");
                        e.MapRightKey("MapId");
                        e.ToTable("EventsMapPool");
                    });
            //modelBuilder.Entity<Event>().HasMany(p => p.Sponsors).WithMany(e => e.Events).Map(
            //    e =>
            //        {
            //            e.MapLeftKey("EventId");
            //            e.MapRightKey("SponsorId");
            //            e.ToTable("EventsSponsors");
            //        });
            modelBuilder.Entity<Sponsor>().HasRequired(p => p.User).WithRequiredDependent(e => e.Sponsor);
            modelBuilder.Entity<Article>().HasRequired(p => p.Author).WithMany(e => e.Articles);
            modelBuilder.Entity<Article>().HasMany(p => p.Images).WithRequired(e => e.Article);
            modelBuilder.Entity<Article>().HasMany(p => p.Videos).WithRequired(e => e.Article);
            modelBuilder.Entity<Match>().HasMany(p => p.Images).WithRequired(e => e.Match);
            modelBuilder.Entity<Match>().HasMany(p => p.Videos).WithRequired(e => e.Match);

            base.OnModelCreating(modelBuilder);
        }
    }
}
